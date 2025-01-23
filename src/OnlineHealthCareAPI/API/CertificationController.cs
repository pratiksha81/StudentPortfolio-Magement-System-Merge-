using API;
using Application.Features.Certification.Command;
using Application.Features.Certification.Query;
using Application.Features.HealthTest.Command;
using Application.Features.HealthTest.Query;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace StudentPortfolio_Management_System.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class CertificationController : BaseApiController
    {
        //private readonly IMediator _mediator;
        private readonly ILogger<CertificationController> _logger;

        public CertificationController(ILogger<CertificationController> logger)
        {
            _logger = logger;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllCertificate([FromQuery] CertificationListQuery query)
        {
            try
            {
                var response = await Mediator.Send(query);

                return Ok(response);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }

        }


        [HttpPost("Create")]
        public async Task<IActionResult> AddCertification([FromForm] AddCertificationCommand command)
        {
            try
            {
                var Certification = await Mediator.Send(command);
                return Ok(Certification);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }



    }
}
