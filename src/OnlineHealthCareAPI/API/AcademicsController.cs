using API;
using Application.Dto.Academics;
using Application.Features.Academics.Command;
using Application.Features.Academics.Query;
using Domain.Entities;
using FluentValidation;
using Infrastructure.Persistence.Contexts;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace StudentPortfolio_Management_System.API
{
    [Route("academics")]
    [ApiController]
    public class AcademicsController : BaseApiController
    {
        private readonly IValidator<Academics> _academicValidator;
        private readonly ApplicationDbContext _context;

        public AcademicsController(IValidator<Academics> academicValidator, ApplicationDbContext context)
        {
            _academicValidator = academicValidator;
            _context = context;
        }

        [HttpGet]
        public async Task<ActionResult<IQueryable<object>>> GetAllAcademics([FromQuery] string section = null, [FromQuery] double? gpa = null)
        {
            var query = _context.Academics.Include(a => a.Student).AsQueryable();

            if (!string.IsNullOrEmpty(section))
            {
                query = query.Where(a => a.Section == section);
            }

            if (gpa.HasValue)
            {
                query = query.Where(a => a.GPA == gpa.Value);
            }

            var academics = await query
                .Select(a => new
                {
                    a.Id,
                    a.Section,
                    a.GPA,
                    a.Joined,
                    a.Ended,
                    a.Semester,
                    a.Scholarship,
                    StudentName = a.Student.Name // Include the student's name
                })
                .ToListAsync();

            return Ok(academics);
        }

        [HttpGet("GetAcademicById/{id}")]
        public async Task<ActionResult<object>> GetAcademicById(int id)
        {
            var academic = await _context.Academics
                .Include(a => a.Student)
                .Where(a => a.Id == id)
                .Select(a => new
                {
                    a.Id,
                    a.Section,
                    a.GPA,
                    a.Joined,
                    a.Ended,
                    a.Semester,
                    a.Scholarship,
                    StudentName = a.Student.Name // Include the student's name
                })
                .FirstOrDefaultAsync();

            return academic != null ? Ok(academic) : NotFound();
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] AcademicRegistrationCommand command)
        {
            try
            {
                var academicEntity = new Academics
                {
                    StudentId = command.AcademicsDto.StudentId,
                    Section = command.AcademicsDto.Section,
                    GPA = command.AcademicsDto.GPA,
                    Joined = command.AcademicsDto.Joined,
                    Ended = command.AcademicsDto.Ended,
                    Semester = command.AcademicsDto.Semester,
                    Scholarship = command.AcademicsDto.Scholarship
                };

                var validationResult = await _academicValidator.ValidateAsync(academicEntity);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
                }

                var response = await Mediator.Send(command);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { succeeded = false, message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateAcademic(int id, [FromForm] AcademicUpdateCommand command)
        {
            try
            {
                if (id != command.UpdateAcademicDto.AcademicId)
                {
                    return BadRequest("Academic ID mismatch.");
                }

                var academicEntity = new Academics
                {
                    Id = command.UpdateAcademicDto.AcademicId,
                    Section = command.UpdateAcademicDto.Section,
                    GPA = command.UpdateAcademicDto.GPA,
                    Joined = command.UpdateAcademicDto.Joined,
                    Ended = command.UpdateAcademicDto.Ended,
                    Semester = command.UpdateAcademicDto.Semester,
                    Scholarship = command.UpdateAcademicDto.Scholarship
                };

                var validationResult = await _academicValidator.ValidateAsync(academicEntity);

                if (!validationResult.IsValid)
                {
                    return BadRequest(validationResult.Errors.Select(e => e.ErrorMessage));
                }

                var response = await Mediator.Send(command);

                return response != null ? Ok(response) : NotFound("Academic record not found.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { success = false, message = $"Internal server error: {ex.Message}" });
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteAcademics(int id)
        {
            try
            {
                var command = new AcademicDeleteCommand { AcademicId = id };
                var response = await Mediator.Send(command);

                if (!response)
                {
                    return NotFound("Academic record not found or could not be deleted.");
                }

                return Ok("Academic record deleted successfully.");
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { message = $"Internal server error: {ex.Message}" });
            }
        }
    }
}
