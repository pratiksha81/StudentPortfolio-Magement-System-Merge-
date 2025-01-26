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
    public class CreateProjectCommand : CreateProjectDto, IRequest<CreateProjectDto>
    {
       
    }

    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, CreateProjectDto>
    {
        private readonly IProjectService _projectService;

        public CreateProjectCommandHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<CreateProjectDto> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            var project = await _projectService.CreateProjectAsync(request);
            return project;
        }

    
    }
}
