using Application.Dto.Notice;
using Application.Interfaces.Services.NoticeService;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notice.Query
{
    public class GetNoticeByIdQuery : IRequest<NoticeDto>
    {
        public int NoticeId { get; set; }
    }

    public class GetNoticeByIdQueryHandler : IRequestHandler<GetNoticeByIdQuery, NoticeDto>
    {
        private readonly INoticeService _noticeService;

        public GetNoticeByIdQueryHandler(INoticeService noticeService)
        {
            _noticeService = noticeService;
        }

        public async Task<NoticeDto> Handle(GetNoticeByIdQuery request, CancellationToken cancellationToken)
        {
            return await _noticeService.GetNoticeByIdAsync(request.NoticeId);
        }
    }

}
