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
    public class GetAllNoticesQuery : IRequest<IQueryable<NoticeDto>>
    {
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 10;
    }

    public class GetAllNoticesQueryHandler : IRequestHandler<GetAllNoticesQuery, IQueryable<NoticeDto>>
    {
        private readonly INoticeService _noticeService;

        public GetAllNoticesQueryHandler(INoticeService noticeService)
        {
            _noticeService = noticeService;
        }

        public async Task<IQueryable<NoticeDto>> Handle(GetAllNoticesQuery request, CancellationToken cancellationToken)
        {
            return await _noticeService.GetAllNoticesAsync(request.PageNumber, request.PageSize);
        }
    }
}
