using System.Net;
using ClassManagement.Api.DTO.Notifies;
using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.Services.Notifies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities.Common;

namespace ClassManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class NotifiesController(INotifyService notifyService, IConfiguration configuration, ILogger<NotifiesController> logger) : ControllerBase
    {
        private readonly INotifyService _notifyService = notifyService;

        private readonly IConfiguration _configuration = configuration;

        private readonly ILogger<NotifiesController> _logger = logger;
        /// <summary>
        /// Get The Notifications
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] NotifyPageRequest request)
        {
            _logger.LogInformation("Get notifies with {@request}", request);

            _logger.LogInformation(string.Format("GET RESULT = {0}", HttpStatusCode.OK));

            return Ok(await _notifyService.GetAsync(request));
        }

        /// <summary>
        /// Get Notification By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            _logger.LogInformation("Get notify {@id}", id);

            if (string.IsNullOrEmpty(id)) return BadRequest();

            var result = await _notifyService.GetByIdAsync(id);

            if (result is null) return NotFound();

            _logger.LogInformation(string.Format("GET RESULT = {0}", HttpStatusCode.OK));

            return Ok(result);
        }

        /// <summary>
        /// Create A New Notification
        /// </summary>
        /// <remarks>
        /// Using a new CreateNotifyRequest model
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Policy = PolicyConstants.REQUIRELOWERROLES)]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateNotifyRequest request)
        {
            _logger.LogInformation("Create notify with {@request}", request);

            var notifyId = await _notifyService.CreateAsync(request);

            if (string.IsNullOrEmpty(notifyId)) return BadRequest();

            _logger.LogInformation(string.Format("POST RESULT = {0}", HttpStatusCode.OK));

            return Created(new Uri($"{_configuration["BaseAddress"]}/api/notifies/{notifyId}"), await _notifyService.GetByIdAsync(notifyId));
        }

        /// <summary>
        /// Update A Notification
        /// </summary>
        /// <remarks>
        /// Using a new UpdateNotifyRequest model
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Policy = PolicyConstants.REQUIRELOWERROLES)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, [FromForm] UpdateNotifyRequest request)
        {
            _logger.LogInformation("Update notify {@id} with {@request}", id, request);

            if (string.IsNullOrEmpty(id)) return BadRequest();

            var result = await _notifyService.UpdateAsync(id, request);

            if (!result) return BadRequest();

            _logger.LogInformation(string.Format("PUT RESULT = {0}", HttpStatusCode.OK));

            return Ok();
        }

        /// <summary>
        /// Change The Status Of The Notification
        /// </summary>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Policy = PolicyConstants.REQUIRELOWERROLES)]
        [HttpPut("change-status/{id}")]
        public async Task<IActionResult> ChangeStatusAsync(string id, [FromBody] ChangeNotifyStatusRequest request)
        {
            _logger.LogInformation("Change notify {@id} status with {@request}", id, request);

            if (string.IsNullOrEmpty(id) || request.UserId <= 0) return BadRequest();

            var result = await _notifyService.ChangeStatusAsync(id, request);

            if (!result) return BadRequest();

            _logger.LogInformation(string.Format("PUT RESULT = {0}", HttpStatusCode.OK));

            return Ok();
        }

        /// <summary>
        /// Delete A Notification
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [Authorize(Roles = RoleConstants.ADMIN_NAME)]
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteAsync(string id)
        {
            _logger.LogInformation("Delete notify {@id}", id);

            if (string.IsNullOrEmpty(id)) return BadRequest();

            var result = await _notifyService.DeleteAsync(id);

            if (!result) return BadRequest();

            _logger.LogInformation(string.Format("DELETE RESULT = {0}", HttpStatusCode.OK));

            return NoContent();
        }
    }
}
