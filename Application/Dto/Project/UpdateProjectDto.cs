using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Project
{
    public class UpdateProjectDto
    {
        [Required]
        public int Id { get; set; }
        [Required]
        public string ProjectTitle { get; set; }
        [Required]
        public string ProjectTools { get; set; }
        [Required]
        public string ProjectDuration { get; set; }
        [Required]
        public string ProjectDescription { get; set; }
    }
}