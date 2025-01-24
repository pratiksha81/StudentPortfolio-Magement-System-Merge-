namespace Domain.Entities
{
    public class Academics
	{

		public int Id { get; set; }
		public string Section { get; set; }
		public double GPA { get; set; }
		public DateTime Joined { get; set; }
		public DateTime Ended { get; set; }
		public string Semester { get; set; }
		public double Scholarship { get; set; }

        // Foreign key
        public int StudentId { get; set; }

        // Navigation property
        public Student Student { get; set; }


    }
}


