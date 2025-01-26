using Application.Dto.Notice;
using Application.Interfaces.Services.NoticeService;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Notice.Command
{
    public class AddNoticeCommand : IRequest<int>
    {
        public AddNoticeDto Notice { get; set; }
    }

    public class AddNoticeCommandHandler : IRequestHandler<AddNoticeCommand, int>
    {
        private readonly INoticeService _noticeService;

        public AddNoticeCommandHandler(INoticeService noticeService)
        {
            _noticeService = noticeService;
        }

        public async Task<int> Handle(AddNoticeCommand request, CancellationToken cancellationToken)
        {
            var noticeId = await _noticeService.AddNoticeAsync(request.Notice);
            return noticeId;
        }
    }
}
