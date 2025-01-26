using API;
using Application.Dto.Portfolio;
using Application.Dto.Project;
using Application.Dto.Student;
using Application.Features.Portfolio.Command;
using Application.Features.Portfolio.Query;
using Application.Features.Project.Command;
using Application.Features.Project.Query;
using Application.Features.Student.Query;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentPortfolio_Management_System.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ProjectController : BaseApiController
    {
        [HttpGet("{id}")]
        public async Task<ActionResult<ProjectDto>> GetProjectById(int id)
        {
            var result = await Mediator.Send(new GetProjectByIdQuery { Id = id });
            return result;
        }

        [HttpGet]
        public async Task<ActionResult<IQueryable<ProjectDto>>> GetAllProject()
        {
            var portfolios = await Mediator.Send(new GetAllProjectQuery());
            return Ok(portfolios);
        }

        [HttpPost]
        public async Task<IActionResult> AddProject(CreateProjectCommand command)
        {
            // If validation passes, proceed with adding the student

            var projectId = await Mediator.Send(command);
            return Ok(projectId);
        }

        [HttpPut]
        public async Task<IActionResult> UpdateProject(int id, [FromForm] UpdateProjectCommand updateProjectDto)
        {
            if (updateProjectDto == null)
                return BadRequest("Invalid request");

            updateProjectDto.Id = id;                                   // Assign the ID from the route to the command
            var result = await Mediator.Send(updateProjectDto);

            return result ? Ok("Update successful") : NotFound("Project not found");
        }

        [HttpDelete]
        public async Task<IActionResult> DeleteProject(int id)
        {
            var result = await Mediator.Send(new DeleteProjectCommand { Id = id });
            return Ok(result);





        }
    }
}
