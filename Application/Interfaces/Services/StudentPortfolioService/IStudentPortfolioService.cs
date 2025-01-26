using Application.Dto.StudentPortfolio;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.StudentPortfolioService
{
    public interface IStudentPortfolioService
    {
        Task<StudentPortfolioDto> GetStudentPortfolioAsync(int studentId);
    }
}
