using Application.Dto.Academics;
using Application.Dto.Teacher;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.AcademicsService
{
    public interface IAcademicsService
    {
		Task<AcademicsDto> RegisterAcademics(AcademicsDto academicsDtoDto); // For adding a new teacher
		Task<AcademicsDto> GetAcademicsByIdAsync(int AcademicId); // For retrieving teacher details by ID
                                                                  //													 // Task<IEnumerable<TeacherDto>> GetAllTeacherAsync();//retrive all the details
       // Task<IQueryable<AcademicsDto>> GetAllAcademicsAsync(string Section = null, double? GPA = null);
        Task<AcademicsDto> UpdateAcademics(AcademicsDto academicsDtoDto); // For updating teacher details
		Task<bool> DeleteAcademics(int AcademicId); // For deleting a teacher by ID
        Task<IQueryable<AcademicsDto>> GetAllAcademicsAsync(double? GPA, string Section);
        // For fetching combined student and academic details by studentId and name4

        

    }
}
