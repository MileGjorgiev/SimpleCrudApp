using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SimpleCrudApp.BLL.Abstract;
using SimpleCrudApp.Models.Entities;
using System.Security.Claims;


namespace SimpleCrudApp.API.Controllers.V1
{
    [Route("api/v1/project")]
    [ApiController]
    [EnableCors("AllowAllOrigins")]
    public class ProjectController : ControllerBase
    {
        private readonly IProjectService _projectService;

        public ProjectController(IProjectService projectService)
        {
            _projectService = projectService;
        }


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> GetAll()
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                List<Project> projects = await _projectService.GetAllAsync(userId);
                return new JsonResult(projects);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "An unexpected error occurred while fetching projects.");
            }
        }

        [HttpGet("{projectId}")]
        [Authorize]
        public async Task<IActionResult> Get(int projectId)
        {
            try
            {
                Project project = await _projectService.GetAsync(projectId);

                if (project == null)
                {
                    return NotFound(new { error = $"Project with ID {projectId} not found." });
                }

                return new JsonResult(project);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new { error = "An unexpected error occurred." });
            }
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Save([FromBody] Project project)
        {
            try
            {
                var userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
                await _projectService.SaveAsync(project, userId);

                return new JsonResult(new
                {
                    projectId = project.ProjectId
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

        [HttpDelete("{projectId}")]
        [Authorize]
        public async Task<IActionResult> Delete(int projectId)
        {
            try
            {
                await _projectService.DeleteAsync(projectId);
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
