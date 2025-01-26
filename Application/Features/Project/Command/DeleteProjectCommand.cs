using Application.Interfaces.Services.ProjectService;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Xsl;

namespace Application.Features.Project.Command
{
    public class DeleteProjectCommand : IRequest<bool>
    {
        public int Id { get; set; } 
    }

    public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, bool>
    {
        private readonly IProjectService _projectService;

        public DeleteProjectCommandHandler(IProjectService projectService)
        {
            _projectService = projectService;
        }

        public async Task<bool> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
        {
            var result = await _projectService.DeleteProjectAsync(request.Id);
            return result;
        }
    }
}
