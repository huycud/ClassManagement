using System.Net;
using ClassManagement.Api.DTO.Department;
using ClassManagement.Api.DTO.Page;
using ClassManagement.Api.Services.Departments;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Utilities.Common;

namespace ClassManagement.Api.Controllers
{
    [Authorize(Roles = RoleConstants.ADMIN_NAME)]
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentsController(IDepartmentService departmentService, IConfiguration configuration, ILogger<DepartmentsController> logger) : ControllerBase
    {
        private readonly IDepartmentService _departmentService = departmentService;

        private readonly IConfiguration _configuration = configuration;

        private readonly ILogger<DepartmentsController> _logger = logger;

        /// <summary>
        /// Get Departments
        /// </summary>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpGet]
        public async Task<IActionResult> GetAsync([FromQuery] CommonPageRequest request)
        {
            _logger.LogInformation("Get departments with {@request}", request);

            _logger.LogInformation(string.Format("GET RESULT = {0}", HttpStatusCode.OK));

            return Ok(await _departmentService.GetAsync(request));
        }

        //[HttpGet("list")]
        //public async Task<IActionResult> GetAsync()
        //{
        //    return Ok(await _departmentService.GetAsync());
        //}

        /// <summary>
        /// Get Department By Id
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("{id}")]
        public async Task<IActionResult> GetByIdAsync(string id)
        {
            _logger.LogInformation("Get department {@id}", id);

            if (string.IsNullOrEmpty(id)) return BadRequest();

            var result = await _departmentService.GetByIdAsync(id);

            if (result is null) return NotFound();

            _logger.LogInformation(string.Format("GET RESULT = {0}", HttpStatusCode.OK));

            return Ok(result);
        }

        /// <summary>
        /// Create A New Department
        /// </summary>
        /// <remarks>
        /// Using a new CreateDepartmentRequest model
        /// </remarks>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPost]
        public async Task<IActionResult> CreateAsync([FromBody] CreateDepartmentRequest request)
        {
            _logger.LogInformation("Create department with {@request}", request);

            var departmentId = await _departmentService.CreateAsync(request);

            if (string.IsNullOrEmpty(departmentId)) return BadRequest();

            _logger.LogInformation(string.Format("POST RESULT = {0}", HttpStatusCode.OK));

            return Created(new Uri($"{_configuration["BaseAddress"]}/api/departments/{departmentId}"), await _departmentService.GetByIdAsync(departmentId));
        }

        /// <summary>
        /// Update Department
        /// </summary>
        /// <remarks>
        /// Using a UpdateDepartmentRequest new model
        /// </remarks>
        /// <param name="id"></param>
        /// <param name="request"></param>
        /// <returns></returns>
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateAsync(string id, [FromBody] UpdateDepartmentRequest request)
        {
            _logger.LogInformation("Update department {@id} with {@request}", id, request);

            if (string.IsNullOrEmpty(id)) return BadRequest();

            var result = await _departmentService.UpdateAsync(id, request);

            if (!result) return BadRequest();

            _logger.LogInformation(string.Format("PUT RESULT = {0}", HttpStatusCode.OK));

            return Ok();
        }
    }
}
