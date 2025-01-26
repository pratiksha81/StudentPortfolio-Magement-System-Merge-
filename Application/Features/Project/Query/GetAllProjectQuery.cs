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
    public class GetAllProjectQuery : IRequest<IEnumerable<ProjectDto>>
    {

    }
    
    public class GetAllProjectHandlers : IRequestHandler<GetAllProjectQuery, IEnumerable<ProjectDto>>
    {
        private readonly IProjectService _projectService;

        public GetAllProjectHandlers(IProjectService projectService)
        {
            _projectService = projectService;
        }
        public async Task<IEnumerable<ProjectDto>> Handle(GetAllProjectQuery request, CancellationToken cancellationToken)
        {
            return await _projectService.GetAllProjectAsync();
        }
    }
    
}
