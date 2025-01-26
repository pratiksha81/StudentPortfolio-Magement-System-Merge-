using Application.Dto.Patient;
using Application.Dto.Student;
using Application.Dto.Teacher;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.TeacherService
{
    public interface ITeacherService
    {
        Task<(IQueryable<TeacherDto>teachers, int totalcount)> GetAllTeachersAsync(string qualification = null, string experience = null, int pageNumber = 1, int pageSize = 5);

        Task<AddTeacherDto> RegisterTeacher(AddTeacherDto teacherDto); // For adding a new teacher
        Task<TeacherDto> GetTeacherById(int teacherId); // For retrieving teacher details by ID
        Task<UpdateTeacherDto> UpdateTeacher(UpdateTeacherDto teacherDto); // For updating teacher details
        Task<bool> DeleteTeacher(int teacherId); // For deleting a teacher by ID
    }
}