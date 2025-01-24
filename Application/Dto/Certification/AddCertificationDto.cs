namespace Application.Dto.Certification
{
    public class AddCertificationDto
    {
        //public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string ReceivedDate { get; set; }

        public int StudentId { get; set; }
    }
}
