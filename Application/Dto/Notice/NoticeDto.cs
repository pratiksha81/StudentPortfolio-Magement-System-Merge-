using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Notice
{
    public class NoticeDto
    {
        public int NoticeId { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        public DateTime Date { get; set; }
        public string ImageUrl { get; set; }
    }
}
