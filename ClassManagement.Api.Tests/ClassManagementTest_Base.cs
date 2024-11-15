using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using ClassManagement.Api.Data.Entities;
using ClassManagement.Api.DTO.Users.Manager;
using ClassManagement.Api.Tests.TestData;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Moq;
using Utilities.Common;

namespace ClassManagement.Api.Tests
{
    public class ClassManagementTest_Base : IClassFixture<BaseFixture>
    {
        protected readonly BaseFixture _fixture;

        protected readonly Mock<UserManager<User>> _adminManagerMock;

        protected readonly Mock<IMapper> _mapperMock = new Mock<IMapper>();

        protected readonly IConfiguration _configuration;

        protected readonly HttpClient _httpClient;

        public ClassManagementTest_Base(BaseFixture fixture)
        {
            _fixture = fixture;

            _configuration = _fixture.Configuration;

            _httpClient = _fixture.GetHttpClient();

            _adminManagerMock = new Mock<UserManager<User>>(

                                new Mock<IUserStore<User>>().Object,

                                new Mock<IOptions<IdentityOptions>>().Object,

                                new Mock<IPasswordHasher<User>>().Object,

                                new IUserValidator<User>[0],

                                new IPasswordValidator<User>[0],

                                new Mock<ILookupNormalizer>().Object,

                                new Mock<IdentityErrorDescriber>().Object,

                                new Mock<IServiceProvider>().Object,

                                new Mock<ILogger<UserManager<User>>>().Object);

            _adminManagerMock.Setup(userManager => userManager

                            .CreateAsync(It.IsAny<User>(), It.IsAny<string>()))

                            .ReturnsAsync(IdentityResult.Success);

            _adminManagerMock.Setup(userManager => userManager

                            .AddToRoleAsync(It.IsAny<User>(), It.IsAny<string>()))

                            .ReturnsAsync(IdentityResult.Success);

            _mapperMock.Setup(mapper => mapper.Map<User>(It.IsAny<CreateAdminRequest>()))

                        .Returns((CreateAdminRequest request) =>
                        {
                            return new User
                            {
                                UserName = request.UserName,

                                Email = request.Email,

                                CreatedAt = DateTime.UtcNow,

                                UpdatedAt = DateTime.UtcNow,

                                IsDisabled = false,

                                Admin = new Admin { Fullname = request.Fullname },
                            };
                        });

            CheckHighestAuthorizeAsync().Wait();
        }

        protected async Task CheckHighestAuthorizeAsync()
        {
            await IsAuthorizeAsync(TestCommonData.HighestRolesRequest(), TestCommonData.CreateUserRequest(), TestCommonData.ADMIN_ROLE);
        }

        protected async Task CheckAdminAuthorizeAsync()
        {
            await IsAuthorizeAsync(TestCommonData.AdminRolesRequest(), TestCommonData.CreateUserRequest(), TestCommonData.ADMIN_ROLE);
        }

        protected async Task CheckTeacherAuthorizeAsync()
        {
            await IsAuthorizeAsync(TestCommonData.TeacherRolesRequest(), TestCommonData.CreateUserRequest(), TestCommonData.TEACHER_ROLE);
        }

        protected async Task CheckStudentAuthorizeAsync()
        {
            await IsAuthorizeAsync(TestCommonData.StudentRolesRequest(), TestCommonData.CreateUserRequest(), TestCommonData.STUDENT_ROLE);
        }

        protected HttpClient CheckUnauthorized()
        {
            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(SystemConstants.SCHEME_NAME, "");

            return _httpClient;
        }

        private async Task<User> CreateUserAsync(CreateAdminRequest request)
        {
            var user = _mapperMock.Object.Map<User>(request);

            await _adminManagerMock.Object.CreateAsync(user, request.Password);

            return user;
        }

        private async Task AddRoleToUserAsync(User user, string roleName)
        {
            await _adminManagerMock.Object.AddToRoleAsync(user, roleName);
        }

        private string CreateToken(List<Claim> claims)
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:SecretKey"]));

            _ = int.TryParse(_configuration["ValidateJwt:TokenValidityInMinutes"], out int expTime);

            var token = new JwtSecurityToken(

                issuer: _configuration["Jwt:Issuer"],

                audience: _configuration["Jwt:Audience"],

                expires: DateTime.Now.AddMinutes(expTime),

                claims: claims,

                signingCredentials: new SigningCredentials(key, SecurityAlgorithms.HmacSha256));

            return new JwtSecurityTokenHandler().WriteToken(token);
        }

        private async Task<HttpClient> IsAuthorizeAsync(List<string> roles, CreateAdminRequest request, string roleName)
        {
            _adminManagerMock.Setup(userManager => userManager.GetRolesAsync(It.IsAny<User>())).ReturnsAsync(roles);

            var user = await CreateUserAsync(request);

            await AddRoleToUserAsync(user, roleName);

            var listRoles = await _adminManagerMock.Object.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, user.UserName)
            };

            foreach (var role in listRoles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            _httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue(SystemConstants.SCHEME_NAME, CreateToken(claims));

            return _httpClient;
        }
    }
}
