using Application.Dto.StudentPortfolio;
using Application.Interfaces.Repositories.AcademicsRepository;
using Application.Interfaces.Repositories.CertificationRepository;
using Application.Interfaces.Repositories.ExtracurricularActivitiesRepository;
using Application.Interfaces.Services.StudentPortfolioService;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Services.StudentPortfolioService
{
    public class StudentPortfolioService : IStudentPortfolioService
    {
        private readonly ICertificationRepository _certificationRepository;
        private readonly IAcademicsRepository _academicsRepository;
        private readonly IExtracurricularActivitiesRepository _extracurricularActivitiesRepository;

        public StudentPortfolioService(
            ICertificationRepository certificationRepository,
            IAcademicsRepository academicsRepository, IExtracurricularActivitiesRepository extracurricularActivitiesRepository)
        {
            _certificationRepository = certificationRepository;
            _academicsRepository = academicsRepository;
            _extracurricularActivitiesRepository = extracurricularActivitiesRepository;
        }

        public async Task<StudentPortfolioDto> GetStudentPortfolioAsync(int studentId)
        {
            var certifications = await _certificationRepository
                .GetQueryable()
                .Where(c => c.StudentId == studentId)
                .ToListAsync();

            var academics = await _academicsRepository
                .GetQueryable()
                .FirstOrDefaultAsync(a => a.StudentId == studentId);

            var extracurricularActivities = await _extracurricularActivitiesRepository
                .GetQueryable()
                .Where(c=> c.StudentId == studentId).ToListAsync();


            return new StudentPortfolioDto
            {
                Certifications = certifications,
                Academics = academics,
                ExtracurricularActivities= extracurricularActivities    
            };
        }
    }
}
