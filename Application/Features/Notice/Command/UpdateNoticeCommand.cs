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
    public class UpdateNoticeCommand : IRequest<bool>
    {
        public UpdateNoticeDto Notice { get; set; }
    }

    public class UpdateNoticeCommandHandler : IRequestHandler<UpdateNoticeCommand, bool>
    {
        private readonly INoticeService _noticeService;

        public UpdateNoticeCommandHandler(INoticeService noticeService)
        {
            _noticeService = noticeService;
        }

        public async Task<bool> Handle(UpdateNoticeCommand request, CancellationToken cancellationToken)
        {
            var result = await _noticeService.UpdateNoticeAsync(request.Notice);
            return result;
        }
    }
}
