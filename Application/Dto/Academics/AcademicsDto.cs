using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Dto.Academics
{
	public class AcademicsDto
	{
		public int AcademicId { get; set; }  // Unique identifier for the academic record
		public string Section { get; set; } // Section of the student
		public double GPA { get; set; }     // Grade Point Average
		public DateTime Joined { get; set; } // Start date of the academic session
		public DateTime Ended { get; set; }  // End date of the academic session
		public string Semester { get; set; } // Current semester
		public double Scholarship { get; set; } // Scholarship amount
        public int StudentId { get; set; }// Foreign key

    }
}
