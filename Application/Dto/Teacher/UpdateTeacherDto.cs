namespace Application.Dto.Teacher
{
    public class UpdateTeacherDto : AddTeacherDto
    {
        public int Id { get; set; } // Mandatory for identifying the teacher to update

    }
}
