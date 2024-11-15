using System.Net;
using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.DTO.Semester;
using ClassManagement.Api.Services.Semesters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities.Common;

namespace ClassManagement.Api.Controllers
{
    [Authorize(Roles = RoleConstants.ADMIN_NAME)]
    [Route("api/[controller]")]
    [ApiController]
    public class SemestersController(ISemesterService semesterService, IConfiguration configuration, ILogger<SemestersController> logger) : ControllerBase
    {
        private readonly ISemesterService _semesterService = semesterService;

        private readonly IConfiguration _configuration = configuration;

        private readonly ILogger<SemestersController> _logger = logger;

        /// <summary>
        /// Get Semester
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] CommonPageRequest request)
        {
            _logger.LogInformation("Get semesters with {@request}", request);

            _logger.LogInformation(string.Format("GET RESULT = {0}", HttpStatusCode.OK));

            return Ok(await _semesterService.GetAsync(request));
        }

        //[HttpGet("list")]
        //public async Task<IActionResult> GetAsync()
        //{
        //    return Ok(await _semesterService.GetAsync());
        //}

        /// <summary>
        /// Get Semester By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            _logger.LogInformation("Get semester {@id}", id);

            if (string.IsNullOrEmpty(id)) return BadRequest();

            var result = await _semesterService.GetByIdAsync(id);

            if (result is null) return NotFound();

            _logger.LogInformation(string.Format("GET RESULT = {0}", HttpStatusCode.OK));

            return Ok(result);
        }

        /// <summary>
        /// Create A New Semester
        /// </summary>
        /// <remarks>
        /// Using a new CreateSemesterRequest model
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateSemesterRequest request)
        {
            _logger.LogInformation("Create semester with {@request}", request);

            var semesterId = await _semesterService.CreateAsync(request);

            if (string.IsNullOrEmpty(semesterId)) return BadRequest();

            _logger.LogInformation(string.Format("POST RESULT = {0}", HttpStatusCode.OK));

            return Created(new Uri($"{_configuration["BaseAddress"]}/api/semesters/{semesterId}"), await _semesterService.GetByIdAsync(semesterId));
        }

        /// <summary>
        /// Update Semester
        /// </summary>
        /// <remarks>
        /// Using a new UpdateSemesterRequest model
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] UpdateSemesterRequest request)
        {
            _logger.LogInformation("Update semester {@id} with {@request}", id, request);

            if (string.IsNullOrEmpty(id)) return BadRequest();

            var result = await _semesterService.UpdateAsync(id, request);

            if (!result) return BadRequest();

            _logger.LogInformation(string.Format("PUT RESULT = {0}", HttpStatusCode.OK));

            return Ok();
        }
    }
}
