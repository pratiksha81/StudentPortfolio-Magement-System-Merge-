using Application.Dto.StudentPortfolio;
using Application.Interfaces.Repositories.AcademicsRepository;
using Application.Interfaces.Repositories.CertificationRepository;
using Application.Interfaces.Repositories.ExtracurricularActivitiesRepository;
using Application.Interfaces.Repositories.StudentRepository;
using Application.Interfaces.Services.StudentPortfolioService;
using Application.Interfaces.Services.StudentService;
using Infrastructure.Persistence.Repositories.StudentRepository;
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
       // private readonly IStudentRepository _studentRepository;

        public StudentPortfolioService(
            ICertificationRepository certificationRepository,
            IAcademicsRepository academicsRepository, IExtracurricularActivitiesRepository extracurricularActivitiesRepository)
        {
      //      _studentRepository = studentRepository;
            _certificationRepository = certificationRepository;
            _academicsRepository = academicsRepository;
            _extracurricularActivitiesRepository = extracurricularActivitiesRepository;
        }

        public async Task<StudentPortfolioDto> GetStudentPortfolioAsync(int studentId)
        {
            //var student = await _studentRepository
            //     .GetQueryable()
            //     .FirstOrDefaultAsync(s => s.Id == studentId);

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
               // Student = student, // Include student details
                Certifications = certifications,
                Academics = academics,
                ExtracurricularActivities = extracurricularActivities
            };
        }
    }
}
