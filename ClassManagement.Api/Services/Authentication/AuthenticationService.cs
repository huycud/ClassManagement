using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using ClassManagement.Api.Common.Exceptions;
using ClassManagement.Api.Data.Entities;
using ClassManagement.Api.DTO.Authentication;
using ClassManagement.Api.DTO.Common;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Utilities.Common;
using Utilities.Messages;

namespace ClassManagement.Api.Services.Authentication
{
    class AuthenticationService(UserManager<User> userManager, SignInManager<User> signInManager, IConfiguration configuration) : IAuthenticationService
    {
        private readonly UserManager<User> _userManager = userManager;

        private readonly SignInManager<User> _signInManager = signInManager;

        private readonly IConfiguration _configuration = configuration;

        public async Task<Response> LoginAsync(LoginRequest request)
        {
            var entity = await _userManager.FindByNameAsync(request.UserName);

            if (entity is null || entity.IsDisabled) throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "UserName"));

            //var res = await _userManager.GetLockoutEndDateAsync(entity);

            //DateTimeOffset? dateTimeOffset = res.HasValue ? res : null;

            //if (dateTimeOffset != null)
            //{
            //    _ = int.TryParse(_configuration["IdentityConfig:LockoutDefaultLockoutTimeSpan"], out int lockoutTime);

            //    if (dateTimeOffset?.AddMinutes(lockoutTime) > DateTime.Now) throw new BadRequestException(string.Format(ErrorMessages.LOCKED, "Account"));
            //}

            var result = await _signInManager.CheckPasswordSignInAsync(entity, request.Password, true);

            if (result.IsLockedOut) throw new ForbiddenException(string.Format(ErrorMessages.LOCKED, "Account"));

            if (result.IsNotAllowed) throw new BadRequestException(string.Format(ErrorMessages.UNCONFIRMED, "Account"));

            if (!result.Succeeded) throw new BadRequestException(ErrorMessages.INVALID, "Password");

            _ = int.TryParse(_configuration["ValidateJwt:AccessTokenValidityInMinutes"], out int accessTokenExpiresTime);

            _ = int.TryParse(_configuration["ValidateJwt:RefreshTokenValidityInMinutes"], out int refreshTokenExpiresTime);

            var accessToken = await CreateTokenAsync(entity, accessTokenExpiresTime);

            var refreshToken = await CreateTokenAsync(entity, refreshTokenExpiresTime);

            var saveRefreshTokenResult = await _userManager.SetAuthenticationTokenAsync(entity, SystemConstants.LOGINPROVIDER_NAME, SystemConstants.REFRESHTOKEN_NAME, refreshToken);

            if (!saveRefreshTokenResult.Succeeded) throw new BadRequestException(string.Format(ErrorMessages.INVALID, SystemConstants.REFRESHTOKEN_NAME));

            return new Response()
            {
                IsSuccess = true,

                Message = string.Format(NotifyMessages.SUCCESS, "Login"),

                AccessToken = accessToken,

                RefreshToken = refreshToken,

                UserId = entity.Id
            };
        }

        public async Task<bool> LogoutAsync(int id)
        {
            var revokedResult = await RevokeRefreshTokenAsync(id);

            return revokedResult;
        }

        public async Task<Response> RefreshANewAccessTokenAsync(string refreshToken)
        {
            (User userEntity, string refToken) = await ValidateTokenAsync(refreshToken);

            _ = int.TryParse(_configuration["ValidateJwt:AccessTokenValidityInMinutes"], out int accessTokenExpiresTime);

            var newAccessToken = await CreateTokenAsync(userEntity, accessTokenExpiresTime);

            return new Response
            {
                IsSuccess = true,

                Message = string.Format(NotifyMessages.SUCCESS, "Create new access token"),

                AccessToken = newAccessToken,

                RefreshToken = refToken,

                UserId = userEntity.Id
            };
        }

        private async Task<string> CreateTokenAsync(User user, int expTime)
        {
            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name,user.UserName),

                new Claim(ClaimTypes.NameIdentifier,user.Id.ToString())
            };

            foreach (var role in roles) claims.Add(new Claim(ClaimTypes.Role, role));

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            var credential = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(

                issuer: _configuration["Jwt:Issuer"],

                audience: _configuration["Jwt:Audience"],

                expires: DateTime.Now.AddMinutes(expTime),

                claims: claims,

                signingCredentials: credential);

            var token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return token;
        }

        private async Task<(User, string)> ValidateTokenAsync(string token)
        {
            var tokenValidationParameters = new TokenValidationParameters
            {
                ValidIssuer = _configuration["Jwt:Issuer"],

                ValidAudience = _configuration["Jwt:Audience"],

                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"])),

                ClockSkew = TimeSpan.Zero
            };

            var tokenHandler = new JwtSecurityTokenHandler();

            var principal = tokenHandler.ValidateToken(token, tokenValidationParameters, out SecurityToken securityToken)

                            ?? throw new BadRequestException(string.Format(ErrorMessages.INVALID, SystemConstants.REFRESHTOKEN_NAME));

            var userId = principal.Claims.FirstOrDefault(x => x.Type == ClaimTypes.NameIdentifier)?.Value;

            var userEntity = await _userManager.FindByIdAsync(userId)

                            ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"));

            var refToken = await _userManager.GetAuthenticationTokenAsync(userEntity, SystemConstants.LOGINPROVIDER_NAME, SystemConstants.REFRESHTOKEN_NAME);

            if (string.IsNullOrEmpty(refToken)) throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, SystemConstants.REFRESHTOKEN_NAME));

            return (userEntity, refToken);
        }

        private async Task<bool> RevokeRefreshTokenAsync(int id)
        {
            var entity = await _userManager.FindByIdAsync(id.ToString())

                                ?? throw new KeyNotFoundException(string.Format(ErrorMessages.NOT_FOUND, "Id"));

            var result = await _userManager.RemoveAuthenticationTokenAsync(entity, SystemConstants.LOGINPROVIDER_NAME, SystemConstants.REFRESHTOKEN_NAME);

            if (!result.Succeeded) return false;

            return true;
        }
    }
}
