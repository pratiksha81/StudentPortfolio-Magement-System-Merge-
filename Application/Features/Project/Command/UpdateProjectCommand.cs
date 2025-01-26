using Application.Dto.Project;
using Application.Interfaces.Services.ProjectService;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Project.Command
{
    public class UpdateProjectCommand : IRequest<bool>
    {
        public int Id { get; set; }
        public string ProjectTitle { get; set; }
        public string ProjectTools { get; set; }
        public string ProjectDuration { get; set; }
        public string ProjectDescription { get; set; }
    }
    public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, bool>
    {
        private readonly IProjectService _projectService;

        public UpdateProjectCommandHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<bool> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
        {
            // Map the command to the DTO
            var projectDto = new UpdateProjectDto
            {
                Id = request.Id,
                ProjectTitle = request.ProjectTitle,
                ProjectTools = request.ProjectTools,
                ProjectDuration = request.ProjectDuration,
                ProjectDescription = request.ProjectDescription
            };

            // Call the service to perform the update
            return await _projectService.UpdateProjectAsync(projectDto);
        }
    }



}
