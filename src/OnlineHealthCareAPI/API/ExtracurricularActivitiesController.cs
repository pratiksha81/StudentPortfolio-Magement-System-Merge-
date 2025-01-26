using API;
using Application.Features.Certification.Query;
using Application.Features.ExtracurricularActivities.Command;
using Application.Features.ExtracurricularActivities.Query;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace StudentPortfolio_Management_System.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class ExtracurricularActivitiesController : BaseApiController
    {
        //private readonly IMediator _mediator;
        private readonly ILogger<ExtracurricularActivitiesController> _logger;

        public ExtracurricularActivitiesController(ILogger<ExtracurricularActivitiesController> logger)
        {
            _logger = logger;
        }

        [HttpGet("getAll")]
        public async Task<IActionResult> GetAllExtracurricularActivities([FromQuery] ExtracurricularActivitiesListQuery query)
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
        public async Task<IActionResult> AddExtracurricularActivities([FromForm] AddExtracurricularActivitiesCommand command)
        {
            try
            {
                var extracurricularActivities = await Mediator.Send(command);
                return Ok(extracurricularActivities);
            }
            catch (Exception ex)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }

        [HttpGet("student/{studentId}/ExtracurricularActivities")]
        public async Task<IActionResult> GetExtracurricularActivitiesByStudentId(int studentId)
        {
            try
            {
                
                var extracurricularActivities = await Mediator.Send(new ExtracurricularActivitiesByStudentIdListQuery { StudentId = studentId });

                return Ok(extracurricularActivities);
            }
            catch (Exception ex)
            {
                
                return StatusCode(StatusCodes.Status500InternalServerError, "Internal Server Error");
            }
        }


    }
}
