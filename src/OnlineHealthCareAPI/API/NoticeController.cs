using API;
using Application.Dto.Notice;
using Application.Features.Notice.Command;
using Application.Features.Notice.Query;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace StudentPortfolio_Management_System.API
{
    [Route("api/[controller]")]
    [ApiController]
    public class NoticeController : BaseApiController
    {
        private readonly IValidator<AddNoticeDto> _validator;

        public NoticeController(IValidator<AddNoticeDto> validator)
        {
            _validator = validator;
        }

        [HttpGet]
        public async Task<ActionResult<IQueryable<NoticeDto>>> GetAllNotices(
            [FromQuery] int pageNumber = 1,
            [FromQuery] int pageSize = 10)
        {
            var notices = await Mediator.Send(new GetAllNoticesQuery
            {
                PageNumber = pageNumber,
                PageSize = pageSize
            });

            return Ok(notices);
        }

        [HttpPost]
        public async Task<IActionResult> AddNotice(AddNoticeDto noticeDto)
        {
            var validationResult = await _validator.ValidateAsync(noticeDto);
            if (!validationResult.IsValid)
            {
                return BadRequest(validationResult.Errors);
            }

            var noticeId = await Mediator.Send(new AddNoticeCommand { Notice = noticeDto });
            return Ok(noticeId);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<NoticeDto>> GetNoticeById(int id)
        {
            var noticeDto = await Mediator.Send(new GetNoticeByIdQuery { NoticeId = id });
            return noticeDto != null ? Ok(noticeDto) : NotFound();
        }

        [HttpPut("Update/{id}")]
        public async Task<IActionResult> UpdateNotice(int id, [FromForm] UpdateNoticeCommand noticeDto)
        {
            var result = await Mediator.Send(noticeDto);
            return result ? Ok(result) : NotFound();
        }

        //[HttpDelete("Delete/{id}")]
        //public async Task<IActionResult> DeleteNotice(int id)
        //{
        //    var result = await Mediator.Send(new DeleteNoticeCommand { Id = id });
        //    return result ? Ok(result) : NotFound();
        //}
    }
}