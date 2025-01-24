using Application.Dto.Academics;
using Application.Dto.Student;
using Application.Interfaces.Repositories.AcademicsRepository;
using Application.Interfaces.Services.AcademicsService;
using Application.Interfaces.Services.StudentService;
using Domain.Entities;

namespace Infrastructure.Persistence.Services.AcademicsService
{
    public class AcademicsService : IAcademicsService
	{
		private readonly IAcademicsRepository _academicsRepository;
        private readonly IStudentService _studentService;

        public AcademicsService(IAcademicsRepository academicsRepository, IStudentService StudentService)
		{
			_academicsRepository = academicsRepository;
			_studentService = StudentService;
        }
        public async Task<bool> DeleteAcademics(int academicId)
		{
			var academic = await _academicsRepository.FirstOrDefaultAsync(a => a.Id == academicId);
			if (academic == null)
			{
				throw new Exception("Academic record not found.");
			}

			await _academicsRepository.RemoveAsync(academic);
			await _academicsRepository.SaveChangesAsync();

			return true;
		}

		public async Task<AcademicsDto> GetAcademicsByIdAsync(int academicId)
		{
			var academic = await _academicsRepository.FirstOrDefaultAsync(a => a.Id == academicId);
			if (academic == null)
			{
				throw new Exception("Academic record not found.");
			}

			// Return academic data as an AcademicsDto
			return new AcademicsDto
			{
                StudentId = academic.StudentId,
                AcademicId = academic.Id,
				Section = academic.Section,
				GPA = academic.GPA,
				Joined = academic.Joined,
				Ended = academic.Ended,
				Semester = academic.Semester,
				Scholarship = academic.Scholarship
			};
		}


		public async Task<AcademicsDto> RegisterAcademics(AcademicsDto academicsDto)
		{
            var student = await _studentService.GetStudentByIdAsync(academicsDto.StudentId);
            if (student == null)
            {
                throw new Exception("Student not found");
            }
            var academic = new Academics
			{
				StudentId= academicsDto.StudentId,

                Section = academicsDto.Section,
				GPA = academicsDto.GPA,
				Joined = academicsDto.Joined,
				Ended = academicsDto.Ended,
				Semester = academicsDto.Semester,
				Scholarship = academicsDto.Scholarship
			};

			// Save academic details to the database
			await _academicsRepository.AddAsync(academic);
			await _academicsRepository.SaveChangesAsync();

			// Return the DTO with the newly created record's ID
			academicsDto.AcademicId = academic.Id;

			return academicsDto;
		}


		//public async Task<AcademicsDto> GetAcademicByIdAsync(int Id)
		//{
		//	var academic = await _academicsRepository.FirstOrDefaultAsync(a => a.Id == Id);
		//	if (academic == null)
		//	{
		//		throw new Exception("Academic record not found.");
		//	}

		//	// Return academic data as an AcademicsDto
		//	return new AcademicsDto
		//	{
		//		AcademicId = academic.Id,
		//		Section = academic.Section,
		//		GPA = academic.GPA,
		//		Joined = academic.Joined,
		//		Ended = academic.Ended,
		//		Semester = academic.Semester,
		//		Scholarship = academic.Scholarship
		//	};
		//}


		public async Task<AcademicsDto> UpdateAcademics(AcademicsDto academicDto)
		{
			var academic = await _academicsRepository.FirstOrDefaultAsync(a => a.Id == academicDto.AcademicId);
			if (academic == null)
			{
				throw new Exception("Academic record not found.");
            }

            // Update fields if they are provided in the DTO
            academic.StudentId = academicDto.StudentId != 0 ? academicDto.StudentId : academic.StudentId;

            academic.Section = academicDto.Section ?? academic.Section;
			academic.GPA = academicDto.GPA != 0 ? academicDto.GPA : academic.GPA;
			academic.Joined = academicDto.Joined != default ? academicDto.Joined : academic.Joined;
			academic.Ended = academicDto.Ended != default ? academicDto.Ended : academic.Ended;
			academic.Semester = academicDto.Semester ?? academic.Semester;
			academic.Scholarship = academicDto.Scholarship != 0 ? academicDto.Scholarship : academic.Scholarship;

			await _academicsRepository.UpdateAsync(academic);
			await _academicsRepository.SaveChangesAsync();

			return new AcademicsDto
			{
                StudentId = academic.StudentId,
                AcademicId = academic.Id,
				Section = academic.Section,
				GPA = academic.GPA,
				Joined = academic.Joined,
				Ended = academic.Ended,
				Semester = academic.Semester,
				Scholarship = academic.Scholarship
			};
		}

        public async Task<IQueryable<AcademicsDto>> GetAllAcademicsAsync(double? GPA, string Section)
        {
            var query = _academicsRepository.GetQueryable(); // Using the correct method

            // Apply filter for Section if provided
            if (!string.IsNullOrWhiteSpace(Section))
            {
                query = query.Where(a => a.Section == Section);
            }

            // Apply filter for GPA if provided
            if (GPA.HasValue)
            {
                query = query.Where(a => a.GPA == GPA.Value);
            }

            // Project to AcademicsDto
            var academicsDtos = query.Select(a => new AcademicsDto
            {
                StudentId = a.StudentId,
                AcademicId = a.Id,
                Section = a.Section,
                GPA = a.GPA,
                Joined = a.Joined,
                Ended = a.Ended,
                Semester = a.Semester,
                Scholarship = a.Scholarship
            });

            return await Task.FromResult(academicsDtos); // Return as IQueryable
        }


    }
}
