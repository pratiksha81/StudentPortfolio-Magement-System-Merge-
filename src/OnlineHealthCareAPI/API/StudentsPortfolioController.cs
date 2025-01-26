using API;
using Application.Dto.StudentPortfolio;
using Application.Features.Certification.Query;
using Application.Features.StudentPortfolio.Query;
using Application.Interfaces.Services.StudentPortfolioService;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace StudentPortfolio_Management_System.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsPortfolioController : BaseApiController
    {
        private readonly IStudentPortfolioService _studentPortfolioService;

        public StudentsPortfolioController(IStudentPortfolioService studentPortfolioService)
        {
            _studentPortfolioService = studentPortfolioService;
        }

    
        [HttpGet("by-student/{studentId}")]
        public async Task<ActionResult<List<StudentPortfolioDto>>> GetStudentPortfolioByStudentId(int studentId)
        {
            var query = new StudentPortfolioByStudentIdListQuery { StudentId = studentId };
            var studentPortfolios = await Mediator.Send(query);

            if (studentPortfolios == null || !studentPortfolios.Any())
            {
                return NotFound("No portfolios found for the specified student.");
            }

            return Ok(studentPortfolios);
        }
    }
}
