using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities.Common;

namespace ClassManagement.Api.Controllers
{
    [Authorize(Roles = RoleConstants.STUDENT_NAME)]
    [Route("api/[controller]")]
    [ApiController]
    public class StudentsController : ControllerBase //(IStudentService studentService) : ControllerBase
    {
        //private readonly IStudentService _studentService = studentService;

        //[HttpGet("{id}")]
        //public async Task<IActionResult> GetByIdAsync(int id)
        //{
        //    if (string.IsNullOrEmpty(id.ToString())) return BadRequest();

        //    var result = await _studentService.GetByIdAsync(id);

        //    if (result is null) return NotFound();

        //    return Ok(result);
        //}

        //[HttpPut("{id}")]
        //public async Task<IActionResult> UpdateAsync(int id, [FromBody] UpdateClientInfoRequest request)
        //{
        //    if (string.IsNullOrEmpty(id.ToString())) return BadRequest();

        //    var result = await _studentService.UpdateAsync(id, request);

        //    if (!result) return BadRequest();

        //    return Ok();
        //}

        //[HttpPut("{id}/update-password")]
        //public async Task<IActionResult> UpdatePasswordAsync(int id, [FromBody] UpdatePasswordRequest request)
        //{
        //    if (string.IsNullOrEmpty(id.ToString())) return BadRequest();

        //    var result = await _studentService.UpdatePasswordAsync(id, request);

        //    if (!result) return BadRequest();

        //    return Ok();
        //}

        //[HttpPut("{id}/update-avatar")]
        //public async Task<IActionResult> UpdateImageAsync(int id, [FromForm] UpdateImageRequest request)
        //{
        //    if (string.IsNullOrEmpty(id.ToString())) return BadRequest();

        //    var result = await _studentService.UpdateImageAsync(id, request);

        //    if (!result) return BadRequest();

        //    return Ok();
        //}
    }
}
