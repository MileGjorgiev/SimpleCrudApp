using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleCrudApp.BLL.Abstract;
using SimpleCrudApp.Models.Entities;
using System.Diagnostics.Metrics;

namespace SimpleCrudApp.API.Controllers.V1
{
    [Route("api/v1/task")]
    [ApiController]
    [EnableCors("AllowAllOrigins")]
    public class ProjectTaskController : ControllerBase
    {
        private readonly IProjectTaskService _projectTaskService;

        public ProjectTaskController(IProjectTaskService projectTaskService)
        {
            _projectTaskService = projectTaskService;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                List<ProjectTask> tasks = await _projectTaskService.GetAllAsync();
                return new JsonResult(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while fetching tasks.");
            }
        }

        [HttpGet("project/{projectId}")]
        [Authorize]
        public async Task<IActionResult> GetAllByProjectIdAsync(int projectId)
        {
            try
            {
                List<ProjectTask> tasks = await _projectTaskService.GetAllByProjectIdAsync(projectId);
                return new JsonResult(tasks);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while fetching tasks.");
            }
        }

        [HttpGet("{taskId}")]
        [Authorize]
        public async Task<IActionResult> Get(int taskId)
        {
            try
            {
                ProjectTask task = await _projectTaskService.GetAsync(taskId);

                if (task == null)
                {
                    return NotFound(new { error = $"Task with ID {taskId} not found." });
                }

                return new JsonResult(task);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred." });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Save([FromBody] ProjectTask task)
        {
            try
            {
                await _projectTaskService.SaveAsync(task);

                return new JsonResult(new
                {
                    taskId = task.TaskId
                });
            }
            catch (KeyNotFoundException ex)
            {  
                return NotFound(new { error = ex.Message });
            }
            catch (DbUpdateException ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "A database error occurred." });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred." });
            }
        }

        [HttpDelete("{taskId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int taskId)
        {
            try
            {
                await _projectTaskService.DeleteAsync(taskId);
                return Ok();
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(new { error = ex.Message });
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred." });
            }
        }

    }
}
