using API;
using Application.Dto.AdminProfile;
using Application.Features.AdminProfile.Command;
using Application.Features.AdminProfile.Query;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace YourNamespace.API
{
    [Route("adminprofile")]
    [ApiController]
    public class AdminProfileController : BaseApiController
    {
        //private readonly IValidator<AdminProfileDto> _validator;

        //public AdminProfileController(IValidator<AdminProfileDto> validator)
        //{
        //    _validator = validator;
        //}

        [HttpGet]
        public async Task<ActionResult> GetAllAdminProfiles(
                    [FromQuery] string name = null,
                    [FromQuery] int pageNumber = 1,
                    [FromQuery] int pageSize = 5)
        {
            var result = await Mediator.Send(new GetAllAdminProfilesQuery
            {
                Name = name,
                PageNumber = pageNumber,
                PageSize = pageSize
            });

            var adminProfiles = result.Item1;  // Extract the admin profiles list
            var totalCount = result.Item2;     // Extract the total count

            return Ok(new
            {
                TotalCount = totalCount,
                Data = adminProfiles
            });
        }

        [HttpPost("register")]
        public async Task<IActionResult> AddAdminProfile([FromForm] AddAdminProfileDto adminProfileDto)
        {
            //var validationResult = await _validator.ValidateAsync(adminProfileDto);
            //if (!validationResult.IsValid)
            //{
            //    return BadRequest(validationResult.Errors);
            //}

            var adminProfileId = await Mediator.Send(new AddAdminProfileCommand { AdminProfile = adminProfileDto });
            return Ok(adminProfileId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<AdminProfileDto>> GetAdminProfileById(int id)
        {
            var adminProfileDto = await Mediator.Send(new GetAdminProfileByIdQuery { Id = id });
            return adminProfileDto != null ? Ok(adminProfileDto) : NotFound();
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateAdminProfile(int id,[FromForm] UpdateAdminProfileDto adminProfileDto)
        {
            var result = await Mediator.Send(adminProfileDto);
            return Ok(result);
        }

        [HttpDelete("Delete/{id}")]
        public async Task<IActionResult> DeleteAdminProfile(int id)
        {
            var result = await Mediator.Send(new DeleteAdminProfileCommand { Id = id });
            return result ? Ok(result) : NotFound();
        }
    }
}
