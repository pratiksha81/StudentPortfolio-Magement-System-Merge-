using Application.Interfaces.Repositories.NoticeRepository;
using Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.NoticeRepository
{
    public class NoticeRepository : Repository<Notice>, INoticeRepository
    {
        public NoticeRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
