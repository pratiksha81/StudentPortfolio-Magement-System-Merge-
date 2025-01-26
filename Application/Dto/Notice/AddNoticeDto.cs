using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Notice
{
    public class AddNoticeDto
    {
        public string Title { get; set; }
        public DateTime Date { get; set; }
        public string Description { get; set; }

        public IFormFile Image { get; set; }
    }
}
