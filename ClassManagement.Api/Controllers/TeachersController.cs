using System.Net;
using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.Services.Users.Students;
using ClassManagement.Api.Services.Users.Teachers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities.Common;

namespace ClassManagement.Api.Controllers
{
    [Authorize(Roles = RoleConstants.TEACHER_NAME)]
    [Route("api/[controller]")]
    [ApiController]
    public class TeachersController(ITeacherService teacherService, IStudentService studentService, ILogger<TeachersController> logger) : ControllerBase
    {
        private readonly ITeacherService _teacherService = teacherService;

        private readonly IStudentService _studentService = studentService;

        private readonly ILogger<TeachersController> _logger = logger;

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetByIdAsync(int id)
        //{
        //    if (string.IsNullOrEmpty(id.ToString())) return BadRequest();

        //    var result = await _teacherService.GetByIdAsync(id);

        //    if (result is null) return NotFound();

        //    return Ok(result);
        //}

        //[HttpGet("info/{username}")]
        //public async Task<IActionResult> GetByUsernameAsync(string username)
        //{
        //    if (string.IsNullOrEmpty(username)) return BadRequest();

        //    var result = await _teacherService.GetByUsernameAsync(username);

        //    if (result is null) return NotFound();

        //    return Ok(result);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateClientInfoRequest request)
        //{
        //    if (string.IsNullOrEmpty(id.ToString())) return BadRequest();

        //    var result = await _teacherService.UpdateAsync(id, request);

        //    if (!result) return BadRequest();

        //    return Ok();
        //}

        //[HttpPut("{id}/update-avatar")]
        //public async Task<IActionResult> UpdateImageAsync(int id, [FromForm] UpdateImageRequest request)
        //{
        //    if (string.IsNullOrEmpty(id.ToString())) return BadRequest();

        //    var result = await _teacherService.UpdateImageAsync(id, request);

        //    if (!result) return BadRequest();

        //    return Ok();
        //}

        //[HttpPut("{id}/update-password")]
        //public async Task<IActionResult> UpdatePasswordAsync(int id, [FromBody] UpdatePasswordRequest request)
        //{
        //    if (string.IsNullOrEmpty(id.ToString())) return BadRequest();

        //    var result = await _teacherService.UpdatePasswordAsync(id, request);

        //    if (!result) return BadRequest();

        //    return Ok();
        //}

        /// <summary>
        /// Get students exist in class
        /// </summary>
        /// <remarks>
        /// Using á StudentPageRequest model
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet("get-students-in-class/{id}")]
        public async Task<IActionResult> GetStudentByClassIdAsync(int id, [FromQuery] StudentPageRequest request)
        {
            _logger.LogInformation("Get students in class {@id} with {@request}", id, request);

            if (id <= 0) return BadRequest();

            var result = await _studentService.GetStudentsByClassIdAsync(id, request);

            _logger.LogInformation(string.Format("GET RESULT = {0}", HttpStatusCode.OK));

            return Ok(result);
        }
    }
}
