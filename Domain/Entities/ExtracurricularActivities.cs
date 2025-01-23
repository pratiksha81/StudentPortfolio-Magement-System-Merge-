using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Entities
{
    public class ExtracurricularActivities
    {
        [Key]
        public int Id {  get; set; }
        public string Position { get; set; }
        public string Skill { get; set; }
        public int Year { get; set; }
        public string ClubName { get; set; }
        public string ImageUrl { get; set; }
    }
}
