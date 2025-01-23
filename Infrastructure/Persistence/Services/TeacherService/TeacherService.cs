﻿using Application.Dto.Teacher;
using Application.Interfaces.Repositories.TeacherRepository;
using Application.Interfaces.Services.TeacherService;
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
            teacherDto.Id = teacher.Id;
            teacherDto.ImageUrl = teacher.ImageUrl;

            return teacherDto;
        }
        /*
        public async Task<TeacherDto> RegisterTeacher(TeacherDto request, IFormFile image)
        {
            string imageUrl = null;

            // Handle image upload
            if (image != null)
            {
                if (image.Length > 2 * 1024 * 1024)
                {
                    throw new Exception("Image size exceeds 2 MB.");
                }

                var extension = Path.GetExtension(image.FileName);
                if (!new[] { ".jpg", ".jpeg", ".png" }.Contains(extension.ToLower()))
                {
                    throw new Exception("Supported formats are .jpg, .jpeg, and .png.");
                }

                // Save the image in the "uploads" directory
                var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                if (!Directory.Exists(uploadsFolder))
                {
                    Directory.CreateDirectory(uploadsFolder);
                }

                var fileName = Guid.NewGuid() + extension;
                var filePath = Path.Combine(uploadsFolder, fileName);

                using (var stream = new FileStream(filePath, FileMode.Create))
                {
                    await image.CopyToAsync(stream);
                }

                imageUrl = Path.Combine("uploads", fileName);
            }

            // Create Teacher entity
            var teacher = new Teacher
            {
                Name = request.Name,
                Qualification = request.Qualification,
                Experience = request.Experience,
                Email = request.Email,
                Password = HashPassword(request.Password),
                DOB = request.DOB,
                Gender = request.Gender,
                Phonenumber = request.Phonenumber,
                ImageUrl = imageUrl
            };

            await _teacherRepository.AddAsync(teacher);
            await _teacherRepository.SaveChangesAsync();

            request.Id = teacher.Id;
            request.ImageUrl = imageUrl;
            return request;
        }
        */
        public async Task<TeacherDto> GetTeacherDetails(int teacherId)
        {
            var teacher = await _teacherRepository.FirstOrDefaultAsync(t => t.Id == teacherId);
            if (teacher == null)
            {
                throw new Exception("Teacher not found.");
            }

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

        private string HashPassword(string password)
        {
            using var sha256 = System.Security.Cryptography.SHA256.Create();
            var bytes = sha256.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            return Convert.ToBase64String(bytes);
        }
    }
}
