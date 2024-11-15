using System.Net;
using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.DTO.Subject;
using ClassManagement.Api.Services.Subjects;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities.Common;

namespace ClassManagement.Api.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectsController(ISubjectService subjectService, IConfiguration configuration, ILogger<SubjectsController> logger) : ControllerBase
    {
        private readonly ISubjectService _subjectService = subjectService;

        private readonly IConfiguration _configuration = configuration;

        private readonly ILogger<SubjectsController> _logger = logger;

        /// <summary>
        /// Get Subjects 
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] SubjectDepartmentPageRequest request)
        {
            _logger.LogInformation("Get subjects with {@request}", request);

            _logger.LogInformation(string.Format("GET RESULT = {0}", HttpStatusCode.OK));

            return Ok(await _subjectService.GetAsync(request));
        }

        //[HttpGet("list")]
        //public async Task<IActionResult> GetAsync()
        //{
        //    return Ok(await _subjectService.GetAsync());
        //}

        /// <summary>
        /// Get Subject By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            _logger.LogInformation("Get subject {@id}", id);

            if (string.IsNullOrEmpty(id)) return BadRequest();

            var result = await _subjectService.GetByIdAsync(id);

            if (result is null) return NotFound();

            _logger.LogInformation(string.Format("GET RESULT = {0}", HttpStatusCode.OK));

            return Ok(result);
        }

        /// <summary>
        /// Create A New Subject
        /// </summary>
        /// <remarks>
        /// Using a new CreateSubjectRequest model
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = RoleConstants.ADMIN_NAME)]
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromForm] CreateSubjectRequest request)
        {
            _logger.LogInformation("Create subject with {@request}", request);

            var subjectId = await _subjectService.CreateAsync(request);

            if (string.IsNullOrEmpty(subjectId)) return BadRequest();

            _logger.LogInformation(string.Format("POST RESULT = {0}", HttpStatusCode.OK));

            return Created(new Uri($"{_configuration["BaseAddress"]}/api/classes/{subjectId}"), await _subjectService.GetByIdAsync(subjectId));
        }

        /// <summary>
        /// Update Subject
        /// </summary>
        /// <remarks>
        /// Using a UpdateSubjectRequest model
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [Authorize(Roles = RoleConstants.ADMIN_NAME)]
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, [FromForm] UpdateSubjectRequest request)
        {
            _logger.LogInformation("Update subject {@id} with {@request}", id, request);

            if (string.IsNullOrEmpty(id)) return BadRequest();

            var result = await _subjectService.UpdateAsync(id, request);

            if (!result) return BadRequest();

            _logger.LogInformation(string.Format("PUT RESULT = {0}", HttpStatusCode.OK));

            return Ok();
        }
    }
}
