using ClassManagement.Api.Services.Homeworks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace ClassManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class HomeworksController : ControllerBase
    {
        private readonly IHomeworkService _homeworkService;

        private readonly IConfiguration _configuration;

        public HomeworksController(IHomeworkService homeworkService, IConfiguration configuration)
        {
            _homeworkService = homeworkService;

            _configuration = configuration;
        }

        [HttpGet("get-homeworks-by-class-id/{id}")]
        public async Task<IActionResult> GetHomeworksByClassIdAsync(string id)
        {
            return Ok(await _homeworkService.GetHomeworksByClassId(id));
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(int id)
        {
            if (string.IsNullOrEmpty(id.ToString())) return BadRequest();

            var entity = await _homeworkService.GetByIdAsync(id);

            if (entity is null) return NotFound();

            return Ok(entity);
        }
    }
}
