using Application.Dto.Teacher;
using Application.Interfaces.Repositories.TeacherRepository;
using Application.Interfaces.Services.TeacherService;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System;
using System.IO;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Services.TeacherService
{
    public class TeacherService : ITeacherService
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public TeacherService(ITeacherRepository teacherRepository, IWebHostEnvironment hostingEnvironment)
        {
            _teacherRepository = teacherRepository;
            _hostingEnvironment = hostingEnvironment;
        }


        public async Task<(IQueryable<TeacherDto>teachers, int totalcount)> GetAllTeachersAsync(string qualification = null, string experience = null, int pageNumber = 1, int pageSize = 5)
        {
            var teachersQuery = _teacherRepository.GetQueryable();

            // Apply filters if provided
            if (!string.IsNullOrEmpty(qualification))
            {
                teachersQuery = teachersQuery.Where(t => t.Qualification == qualification);
            }

            if (!string.IsNullOrEmpty(experience))
            {
                teachersQuery = teachersQuery.Where(t => t.Experience == experience);
            }

            var totalcount = teachersQuery.Count();

            // Apply pagination
            teachersQuery = teachersQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            // Map to TeacherDto
            var teacherDtos = teachersQuery.Select(t => new TeacherDto
            {
                Id = t.Id,
                Name = t.Name,
                Qualification = t.Qualification,
                Experience = t.Experience,
                Email = t.Email,
                Password = t.Password,  // It is generally not a good practice to return Password in DTOs. Consider removing it.
                DOB = t.DOB,
                Gender = t.Gender,
                Phonenumber = t.Phonenumber,
                ImageUrl = t.ImageUrl
            });


            return (teacherDtos, totalcount);
        }
        public async Task<AddTeacherDto> RegisterTeacher(AddTeacherDto teacherDto)
        {
            // List to hold the image URLs after validation and saving
            var imageUrls = new List<string>();

            if (teacherDto.Images != null && teacherDto.Images.Any())
            {
                foreach (var image in teacherDto.Images)
                {
                    // Image validation
                    if (image.Length > 2 * 1024 * 1024)  // Check if image size exceeds 2 MB
                    {
                        throw new Exception("Image size exceeds 2 MB.");
                    }

                    var extension = Path.GetExtension(image.FileName);  // Get file extension
                    if (!new[] { ".jpg", ".jpeg", ".png" }.Contains(extension.ToLower()))  // Check if format is allowed
                    {
                        throw new Exception("Supported formats are .jpg, .jpeg, and .png.");
                    }

                    // Save the image in the "uploads" directory
                    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder))
                    {
                        Directory.CreateDirectory(uploadsFolder);
                    }

                    var fileName = Guid.NewGuid() + extension;  // Create a unique file name
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);  // Save the image
                    }

                    // Add the image URL (relative to the web root) to the list
                    imageUrls.Add(Path.Combine("uploads", fileName));
                }
            }

            // Save teacher details
            var teacher = new Teacher
            {
                Name = teacherDto.Name,
                Qualification = teacherDto.Qualification,
                Experience = teacherDto.Experience,
                Email = teacherDto.Email,
                Password = teacherDto.Password,
                DOB = teacherDto.DOB,
                Gender = teacherDto.Gender,
                Phonenumber = teacherDto.Phonenumber,
                ImageUrl = string.Join(";", imageUrls)  // Store the image URLs as a semicolon-separated string
            };

            await _teacherRepository.AddAsync(teacher);
            await _teacherRepository.SaveChangesAsync();

            // Return the DTO with the teacher's data and the image URLs
           // teacherDto.Id = teacher.Id;
            //teacherDto.ImageUrl = teacher.ImageUrl;

            return teacherDto;
        }
   
        public async Task<TeacherDto> GetTeacherById(int teacherId)
        {
            var teacher = await _teacherRepository.FirstOrDefaultAsync(t => t.Id == teacherId);
            if (teacher == null) return null;

            // Return teacher data as a TeacherDto
            return new TeacherDto
            {
                Id = teacher.Id,
                Name = teacher.Name,
                Qualification = teacher.Qualification,
                Experience = teacher.Experience,
                Email = teacher.Email,
                DOB = teacher.DOB,
                Gender = teacher.Gender,
                Phonenumber = teacher.Phonenumber,
                ImageUrl = teacher.ImageUrl
            };
        }



        public async Task<UpdateTeacherDto> UpdateTeacher(UpdateTeacherDto teacherDto)
        {
            var teacher = await _teacherRepository.FirstOrDefaultAsync(t => t.Id == teacherDto.Id);
            if (teacher == null)
            {
                throw new Exception("Teacher not found.");
            }

            // Update fields if they are provided in the DTO
            teacher.Name = teacherDto.Name ?? teacher.Name;
            teacher.Qualification = teacherDto.Qualification ?? teacher.Qualification;
            teacher.Experience = teacherDto.Experience ?? teacher.Experience;
            teacher.Email = teacherDto.Email ?? teacher.Email;
            teacher.Password = !string.IsNullOrWhiteSpace(teacherDto.Password)
                ? HashPassword(teacherDto.Password) : teacher.Password;
            teacher.DOB = teacherDto.DOB ?? teacher.DOB;
            teacher.Gender = teacherDto.Gender ?? teacher.Gender;
            teacher.Phonenumber = teacherDto.Phonenumber ?? teacher.Phonenumber;
           // teacher.ImageUrl = teacherDto.ImageUrl ?? teacher.ImageUrl;

            await _teacherRepository.UpdateAsync(teacher);
            await _teacherRepository.SaveChangesAsync();

            return new UpdateTeacherDto
            {
                Id = teacher.Id,
                Name = teacher.Name,
                Qualification = teacher.Qualification,
                Experience = teacher.Experience,
                Email = teacher.Email,
                DOB = teacher.DOB,
                Gender = teacher.Gender,
                Phonenumber = teacher.Phonenumber,
                //ImageUrl = teacher.ImageUrl
            };
        }


        public async Task<bool> DeleteTeacher(int teacherId)
        {
            var teacher = await _teacherRepository.FirstOrDefaultAsync(t => t.Id == teacherId);
            if (teacher == null)
            {
                throw new Exception("Teacher not found.");
            }

            await _teacherRepository.RemoveAsync(teacher);
            await _teacherRepository.SaveChangesAsync();

            return true;
        }



        private string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}