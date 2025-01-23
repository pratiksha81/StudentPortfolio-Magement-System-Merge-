using Application.Dto.Patient;
using Application.Dto.Teacher;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.TeacherService
{
    public interface ITeacherService
    {
        Task<AddTeacherDto> RegisterTeacher(AddTeacherDto teacherDto);
        Task<TeacherDto> GetTeacherDetails(int teacherId);
    }
}
