using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Notice
{
    public class UpdateNoticeDto : AddNoticeDto
    {
        public int NoticeId { get; set; }
    }
}
