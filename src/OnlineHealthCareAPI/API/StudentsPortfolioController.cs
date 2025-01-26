using Application.Dto.StudentPortfolio;
using Application.Interfaces.Services.StudentPortfolioService;
using Microsoft.AspNetCore.Mvc;

namespace StudentPortfolio_Management_System.API
{
    [ApiController]
    [Route("api/[controller]")]
    public class StudentsPortfolioController : ControllerBase
    {
        private readonly IStudentPortfolioService _studentPortfolioService;

        public StudentsPortfolioController(IStudentPortfolioService studentPortfolioService)
        {
            _studentPortfolioService = studentPortfolioService;
        }

        [HttpGet("{studentId}")]
        public async Task<ActionResult<StudentPortfolioDto>> GetStudentPortfolio(int studentId)
        {
            var studentPortfolio = await _studentPortfolioService.GetStudentPortfolioAsync(studentId);

            if (studentPortfolio == null)
            {
                return NotFound();
            }

            return Ok(studentPortfolio);
        }
    }
}
