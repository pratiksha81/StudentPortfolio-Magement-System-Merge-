using Application.Dto.Student;
using Application.Dto.Teacher;
using Application.Features.Student.Command;
using Application.Features.Teacher.Command;
using Application.Features.Teacher.Query;
using Application.Wrapper;
using Microsoft.AspNetCore.Mvc;

namespace API
{
    [Route("[controller]")]
    [ApiController]
    public class TeacherController : BaseApiController
    {
        //private readonly IValidator<AddTeacherDto> _validator;

        //public TeacherController(IValidator<AddTeacherDto> validator)
        //{
        //    _validator = validator;
        //}

        [HttpGet]
        public async Task<ActionResult<IQueryable<TeacherDto>>> GetAllTeachers(
           [FromQuery] string qualification = null,
           [FromQuery] string experience = null,
           [FromQuery] int pageNumber = 1,
           [FromQuery] int pageSize = 5)
        {
            // Pass the filters and pagination to the query handler
            var teachers = await Mediator.Send(new GetAllTeachersQuery
            {
                Qualification = qualification,
                Experience = experience,
                PageNumber = pageNumber,
                PageSize = pageSize
            });
            var teacher = teachers.Item1;
            var totalcount = teachers.Item2;

            return Ok(new
            {
                TotalCount = totalcount,
                Data = teacher
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> Register([FromForm] AddTeacherDto teach)
        {
            try
            {
                //var validationResult = await _validator.ValidateAsync(command.AddTeacherDto);
                //if (!validationResult.IsValid)
                //{
                //    return BadRequest(validationResult.Errors);
                //}



                var response = await Mediator.Send(new TeacherRegistrationCommand { AddTeacherDto = teach });
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseWrapper(succeeded: false, message: $"Internal server error: {ex.Message}"));
            }
        }
        [HttpGet("GetTeacherById/{id}")]
        public async Task<ActionResult<TeacherDto>> GetTeacherById(int id)
        {
            var result = await Mediator.Send(new GetTeacherByIdQuery { Id = id });
            return result != null ? Ok(result) : NotFound();
        }



        [HttpPut("update/{id}")]
        public async Task<IActionResult> UpdateTeacher(int id, [FromForm] TeacherUpdateCommand command)
        {
            try
            {
                // Ensure the ID in the command matches the route parameter
                if (id != command.UpdateTeacherDto.Id)
                {
                    return BadRequest("Teacher ID mismatch.");
                }

                //// Validate UpdateTeacherDto using the correct validator
                //var validationResult = await _validator.ValidateAsync(command.UpdateTeacherDto);
                //if (!validationResult.IsValid)
                //{
                //    return BadRequest(validationResult.Errors);
                //}

                // Send the update command to the mediator
                var response = await Mediator.Send(command);
                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseWrapper(false, $"Internal server error: {ex.Message}"));
            }
        }

        [HttpDelete("delete/{id}")]
        public async Task<IActionResult> DeleteTeacher(int id)
        {
            try
            {
                // Send the delete command to the mediator
                var command = new TeacherDeleteCommand { TeacherId = id };
                var response = await Mediator.Send(command);

                if (!response)
                {
                    return NotFound(new ResponseWrapper(false, "Teacher not found or could not be deleted."));
                }

                return Ok(new ResponseWrapper(true, "Teacher deleted successfully"));
            }
            catch (Exception ex)
            {
                return StatusCode(500, new ResponseWrapper(false, $"Internal server error: {ex.Message}"));
            }
        }




    }
}