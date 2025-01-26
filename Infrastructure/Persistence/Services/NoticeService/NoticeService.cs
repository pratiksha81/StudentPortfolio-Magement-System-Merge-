using Application.Dto.Notice;
using Application.Interfaces.Repositories.NoticeRepository;
using Application.Interfaces.Services.NoticeService;
using Microsoft.AspNetCore.Hosting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Services.NoticeService
{
    public class NoticeService : INoticeService
    {
        private readonly INoticeRepository _noticeRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public NoticeService(INoticeRepository noticeRepository, IWebHostEnvironment hostingEnvironment)
        {
            _noticeRepository = noticeRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<IQueryable<NoticeDto>> GetAllNoticesAsync(int pageNumber = 1, int pageSize = 10)
        {
            var noticesQuery = _noticeRepository.GetQueryable();

            // Apply pagination
            noticesQuery = noticesQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            var noticeDtos = noticesQuery.Select(notice => new NoticeDto
            {
                NoticeId = notice.NoticeId,
                Title = notice.Title,
                Description = notice.Description,
                Date = notice.Date,
                ImageUrl = notice.ImageUrl
            });

            return noticeDtos;
        }

        public async Task<int> AddNoticeAsync(AddNoticeDto noticeDto)
        {
            // Process image and save it
            string imageUrl = null;
            if (noticeDto.Image != null)
            {
                var extension = Path.GetExtension(noticeDto.Image.FileName);
                var fileName = Guid.NewGuid() + extension;
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await noticeDto.Image.CopyToAsync(stream);
                }

                imageUrl = Path.Combine("uploads", fileName);
            }

            var notice = new Notice
            {
                Title = noticeDto.Title,
                Description = noticeDto.Description,
                Date = noticeDto.Date,
                ImageUrl = imageUrl
            };

            var addedNotice = await _noticeRepository.AddAsync(notice);
            await _noticeRepository.SaveChangesAsync();

            return addedNotice.NoticeId;
        }

        public async Task<NoticeDto> GetNoticeByIdAsync(int noticeId)
        {
            var notice = await _noticeRepository.FirstOrDefaultAsync(x => x.NoticeId == noticeId);
            if (notice == null) return null;

            return new NoticeDto
            {
                NoticeId = notice.NoticeId,
                Title = notice.Title,
                Description = notice.Description,
                Date = notice.Date,
                ImageUrl = notice.ImageUrl
            };
        }

        public async Task<bool> UpdateNoticeAsync(UpdateNoticeDto noticeDto)
        {

            string imageUrl = null;
            if (noticeDto.Image != null)
            {
                var extension = Path.GetExtension(noticeDto.Image.FileName);
                var fileName = Guid.NewGuid() + extension;
                var filePath = Path.Combine(_hostingEnvironment.WebRootPath, "uploads", fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await noticeDto.Image.CopyToAsync(stream);
                }

                imageUrl = Path.Combine("uploads", fileName);
            }

            var notice = await _noticeRepository.FirstOrDefaultAsync(x => x.NoticeId == noticeDto.NoticeId);
            if (notice == null) return false;

            notice.Title = noticeDto.Title;
            notice.Description = noticeDto.Description;
            notice.Date = noticeDto.Date;
            notice.ImageUrl = imageUrl;

            await _noticeRepository.UpdateAsync(notice);
            await _noticeRepository.SaveChangesAsync();

            return true;
        }

        //public async Task<bool> DeleteNoticeAsync(int noticeId)
        //{
        //    var notice = await _noticeRepository.FirstOrDefaultAsync(x => x.NoticeId == noticeId);
        //    if (notice == null) return false;

        //    await _noticeRepository.RemoveAsync(notice);
        //    await _noticeRepository.SaveChangesAsync();

        //    return true;
        //}
    }
}
