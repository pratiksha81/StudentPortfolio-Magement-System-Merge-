using Application.Dto.Portfolio;
using Application.Dto.Project;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.ProjectService
{
    public interface IProjectService
    {
        Task<ProjectDto> GetProjectById(int id);
        Task<IQueryable<ProjectDto>> GetAllProjectAsync();
        Task<CreateProjectDto> CreateProjectAsync(CreateProjectDto createPortfolio);

        Task<bool> UpdateProjectAsync(UpdateProjectDto projectDto);
        Task<bool> DeleteProjectAsync(int projectId);

    }
}
