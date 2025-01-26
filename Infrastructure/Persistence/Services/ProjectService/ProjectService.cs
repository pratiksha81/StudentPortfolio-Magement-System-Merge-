
using Application.Dto.Project;
using Application.Interfaces.Repositories.ProjectRepository;
using Application.Interfaces.Services.ProjectService;
using Application.Interfaces.Services.StudentService;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.WebSockets;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Services.ProjectService
{
    public class ProjectService : IProjectService
    {
        private readonly IProjectRepository _projectRepository;
        private readonly IWebHostEnvironment _webHostEnvironment;


        public ProjectService(IProjectRepository projectRepository, IWebHostEnvironment webHostEnvironment)
        {
            _projectRepository = projectRepository;
            _webHostEnvironment = webHostEnvironment;
        }


        public async Task<ProjectDto> GetProjectById(int id)
        {
            var project = await _projectRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (project == null) 
                return null;

            return new ProjectDto
            {
                Id = project.Id,
                ProjectTitle = project.ProjectTitle,
                ProjectTools = project.ProjectTools,
                ProjectDescription = project.ProjectDescription,
                ProjectDuration = project.ProjectDuration
            };
        }

        // Implement the CreateProjectAsync method
        public async Task<CreateProjectDto> CreateProjectAsync(CreateProjectDto createProjectDto)
        {
            // Map CreateProjectDto to Project entity
            var project = new Project
            {
                ProjectTitle = createProjectDto.ProjectTitle,
                ProjectTools = createProjectDto.ProjectTools,
                ProjectDuration = createProjectDto.ProjectDuration,
                ProjectDescription = createProjectDto.ProjectDescription
            };

            // Add the project to the repository (database)
            await _projectRepository.AddAsync(project);
            await _projectRepository.SaveChangesAsync();

            // Return the Id of the created project
            return new CreateProjectDto
            {
                ProjectTitle = project.ProjectTitle,
                ProjectTools = project.ProjectTools,
                ProjectDuration = project.ProjectDuration,
                ProjectDescription = project.ProjectDescription
            };
        }

      

        public async Task<IQueryable<ProjectDto>> GetAllProjectAsync()
        {
            var projects = _projectRepository.GetQueryable();
            var details = projects.Select(project => new ProjectDto
            {
                Id = project.Id,
                ProjectTitle = project.ProjectTitle,
                ProjectTools = project.ProjectTools,
                ProjectDescription = project.ProjectDescription,
                ProjectDuration = project.ProjectDuration
            });

            return details;
        }

        public async Task<bool> UpdateProjectAsync(UpdateProjectDto projectDto)
        {
            var project = await _projectRepository.FirstOrDefaultAsync(x => x.Id == projectDto.Id);
            if (project == null)
                return false;

            project.ProjectTitle = projectDto.ProjectTitle;
            project.ProjectTools = projectDto.ProjectTools;
            project.ProjectDuration = projectDto.ProjectDuration;
            project.ProjectDescription = projectDto.ProjectDescription;

            await _projectRepository.UpdateAsync(project);
            await _projectRepository.SaveChangesAsync();
            return true;

        }

        public async Task<bool> DeleteProjectAsync(int projectId)
        {
           var project = await _projectRepository.FirstOrDefaultAsync(x => x.Id == projectId);
            if(project == null) return false;

            await _projectRepository.RemoveAsync(project);
            await _projectRepository.SaveChangesAsync();
            return true;
        }
    }
}
