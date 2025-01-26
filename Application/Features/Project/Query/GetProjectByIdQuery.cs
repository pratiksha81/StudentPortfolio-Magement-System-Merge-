using Application.Dto.Project;
using Application.Interfaces.Services.ProjectService;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Project.Query
{
    public class GetProjectByIdQuery : IRequest<ProjectDto>
    {
        public int Id { get; set; }
    }

    public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, ProjectDto>
    {
        private readonly IProjectService _projectService;

        public GetProjectByIdQueryHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }
        public async Task<ProjectDto> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
        {
           return await _projectService.GetProjectById(request.Id);
        }
    }
}
