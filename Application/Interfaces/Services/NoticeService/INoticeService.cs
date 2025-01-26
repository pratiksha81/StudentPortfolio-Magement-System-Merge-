using Application.Dto.Notice;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.NoticeService
{
    public interface INoticeService
    {
        Task<IQueryable<NoticeDto>> GetAllNoticesAsync(int pageNumber = 1, int pageSize = 10);
        Task<NoticeDto> GetNoticeByIdAsync(int noticeId);
        Task<int> AddNoticeAsync(AddNoticeDto noticeDto);
        Task<bool> UpdateNoticeAsync(UpdateNoticeDto noticeDto);
        //Task<bool> DeleteNoticeAsync(int noticeId);
    }
}
