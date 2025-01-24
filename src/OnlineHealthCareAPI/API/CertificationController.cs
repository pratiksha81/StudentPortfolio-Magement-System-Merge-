using API;
using Application.Features.Certification.Command;
using Application.Features.Certification.Query;
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
        public async Task<IActionResult> GetAllCertification([FromQuery] CertificationListQuery query)
        {
            try
            {
                var response = await Mediator.Send(query);
                return Ok(response);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetAllCertification)}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetCertificationById(int id)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetCertificationById)} trigger function received a request for Certification with Id: {id}");
                var certification = await Mediator.Send(new GetCertificationByIdQuery { Id = id });

                if (certification == null)
                {
                    _logger.LogWarning($"Certification with Id {id} not found.");
                    return NotFound();
                }

                _logger.LogInformation($"{nameof(GetCertificationById)} trigger function returned a response {certification}");
                return Ok(certification);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetCertificationById)}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [HttpGet("student/{studentId}/certifications")]
        public async Task<IActionResult> GetCertificationsByStudentId(int studentId)
        {
            try
            {
                _logger.LogInformation($"{nameof(GetCertificationsByStudentId)} trigger function received a request for Student with Id: {studentId}");
                var certifications = await Mediator.Send(new GetCertificationsByStudentIdQuery { StudentId = studentId });

                _logger.LogInformation($"{nameof(GetCertificationsByStudentId)} trigger function returned a response {certifications}");
                return Ok(certifications);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(GetCertificationsByStudentId)}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }
        [HttpPost("Create")]
        public async Task<IActionResult> AddCertification([FromBody] AddCertificationCommand command)
        {
            try
            {
                _logger.LogInformation($"{nameof(AddCertification)} trigger function received a request with param command: '{command}'");
                var certification = await Mediator.Send(command);
                _logger.LogInformation($"{nameof(AddCertification)} trigger function added a new certification with Title: {certification.Title}");

                return Ok(certification);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(AddCertification)}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpPut("Update")]
        public async Task<IActionResult> UpdateCertification([FromBody] UpdateCertificationCommand command)
        {
            try
            {
                _logger.LogInformation($"{nameof(UpdateCertification)} trigger function received a request with param command: '{command}'");
                var isUpdated = await Mediator.Send(command);
                _logger.LogInformation($"{nameof(UpdateCertification)} trigger function updated certification with Id: {command.Id}");

                return Ok(isUpdated);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(UpdateCertification)}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteCertification(int id)
        {
            try
            {
                _logger.LogInformation($"{nameof(DeleteCertification)} trigger function received a request to delete Certification with Id: {id}");
                var deletedCertificationId = await Mediator.Send(new DeleteCertificationCommand { Id = id });
                _logger.LogInformation($"{nameof(DeleteCertification)} trigger function deleted certification with Id: {deletedCertificationId}");

                return Ok(deletedCertificationId);
            }
            catch (Exception ex)
            {
                _logger.LogError($"Error in {nameof(DeleteCertification)}: {ex.Message}");
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }



    }
}
