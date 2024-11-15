using System.Net;
using ClassManagement.Api.DTO.Authentication;
using ClassManagement.Api.Services.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClassManagement.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationsController(IAuthenticationService authenticationService, ILogger<AuthenticationsController> logger) : ControllerBase
    {
        private readonly IAuthenticationService _authenticationService = authenticationService;

        private readonly ILogger<AuthenticationsController> _logger = logger;

        /// <summary>
        /// Login
        /// </summary>
        /// <remarks>
        /// Using a new LoginRequest model
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost("login")]
        public async Task<IActionResult> LoginAsync([FromBody] LoginRequest request)
        {
            _logger.LogInformation("Login with {@request}", request);

            if (string.IsNullOrEmpty(request.UserName) || string.IsNullOrEmpty(request.Password)) return BadRequest();

            var result = await _authenticationService.LoginAsync(request);

            if (!result.IsSuccess) return BadRequest();

            _logger.LogInformation(string.Format("POST RESULT = {0}", HttpStatusCode.OK));

            return Ok(result);
        }

        /// <summary>
        /// Logout
        /// </summary>
        /// <returns></returns>
        [Authorize]
        [HttpPost("logout")]
        public async Task<IActionResult> LogoutAsync(int id)
        {
            _logger.LogInformation("Revoke refresh token with {@id}", id);

            if (id <= 0) return BadRequest();

            var result = await _authenticationService.LogoutAsync(id);

            if (!result) return BadRequest();

            _logger.LogInformation(string.Format("POST RESULT = {0}", HttpStatusCode.OK));

            return Ok();
        }

        /// <summary>
        /// Refresh A New Access Token
        /// </summary>
        /// <remarks>
        /// Using a RefreshToken
        /// </remarks>
        /// <param name="token"></param>
        /// <returns></returns>
        [HttpPost("refresh-token")]
        public async Task<IActionResult> RefreshTokenAsync(string token)
        {
            _logger.LogInformation("Refresh token with {@token}", token);

            if (string.IsNullOrEmpty(token)) return BadRequest();

            var result = await _authenticationService.RefreshANewAccessTokenAsync(token);

            if (!result.IsSuccess) return BadRequest();

            _logger.LogInformation(string.Format("POST RESULT = {0}", HttpStatusCode.OK));

            return Ok(result);
        }
    }
}
