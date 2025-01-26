using Application.Dto.AdminProfile;
using Application.Dto.Student;
using Application.Interfaces;
using Application.Interfaces.Repositories.AdminProfileRepository;
//using Application.Interfaces.Services.AdminProfileService;
using Domain.Entities;
using Microsoft.AspNetCore.Hosting;

namespace Infrastructure.Persistence.Services.AdminProfileService
{
    public class AdminProfileService : IAdminProfileService
    {
        private readonly IAdminProfileRepository _adminProfileRepository;
        private readonly IWebHostEnvironment _hostingEnvironment;

        public AdminProfileService(IAdminProfileRepository adminProfileRepository, IWebHostEnvironment hostingEnvironment)
        {
            _adminProfileRepository = adminProfileRepository;
            _hostingEnvironment = hostingEnvironment;
        }

        public async Task<(IQueryable<AdminProfileDto> adminProfiles, int totalCount)> GetAllAdminProfilesAsync(
            string name = null,
            string email = null,
            int pageNumber = 1,
            int pageSize = 5)
        {
            var adminProfilesQuery = _adminProfileRepository.GetQueryable();

            // Apply filters if provided
            adminProfilesQuery = adminProfilesQuery
                .Where(a => (string.IsNullOrEmpty(name) || (a.FirstName + " " + a.LastName).Contains(name)) &&
                            (string.IsNullOrEmpty(email) || a.Email.Contains(email)));

            // Get the total count before pagination
            var totalCount = adminProfilesQuery.Count();

            // Apply pagination
            adminProfilesQuery = adminProfilesQuery
                .Skip((pageNumber - 1) * pageSize)
                .Take(pageSize);

            // Map to AdminProfileDto
            var adminProfileDtos = adminProfilesQuery.Select(admin => new AdminProfileDto
            {
                Id = admin.Id,
                FirstName = admin.FirstName,
                LastName = admin.LastName,
                Email = admin.Email,
                PhoneNumber = admin.PhoneNumber,
                Address = admin.Address,
                Bio = admin.Bio,
                ImageUrl = admin.ImageUrl
            });

            return (adminProfileDtos, totalCount);
        }

        public async Task<int> AddAdminProfileAsync(AddAdminProfileDto adminProfileDto)
        {
            // List to hold the image URLs after validation and saving
            var imageUrls = new List<string>();

            if (adminProfileDto.Image != null && adminProfileDto.Image.Any())
            {
                foreach (var image in adminProfileDto.Image)
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

            var adminProfile = new AdminProfile
            {
                FirstName = adminProfileDto.FirstName,
                LastName = adminProfileDto.LastName,
                Email = adminProfileDto.Email,
                PhoneNumber = adminProfileDto.PhoneNumber,
                Address = adminProfileDto.Address,
                Bio = adminProfileDto.Bio,
                ImageUrl = string.Join(";", imageUrls)
            };

            var addedAdminProfile = await _adminProfileRepository.AddAsync(adminProfile);
            await _adminProfileRepository.SaveChangesAsync();

            return addedAdminProfile.Id;
        }

        public async Task<AdminProfileDto> GetAdminProfileByIdAsync(int id)
        {
            var adminProfile = await _adminProfileRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (adminProfile == null) return null;

            return new AdminProfileDto
            {
                Id = adminProfile.Id,
                FirstName = adminProfile.FirstName,
                LastName = adminProfile.LastName,
                Email = adminProfile.Email,
                PhoneNumber = adminProfile.PhoneNumber,
                Address = adminProfile.Address,
                Bio = adminProfile.Bio,
                ImageUrl = adminProfile.ImageUrl
            };
        }

        public async Task<bool> UpdateAdminProfileAsync(UpdateAdminProfileDto adminProfileDto)
        {

            // List to hold the image URLs after validation and saving
            var imageUrls = new List<string>();

            if (adminProfileDto.Image != null && adminProfileDto.Image.Any())
            {
                foreach (var image in adminProfileDto.Image)
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

            var adminProfile = await _adminProfileRepository.FirstOrDefaultAsync(x => x.Id == adminProfileDto.Id);
            if (adminProfile == null) return false;

            adminProfile.FirstName = adminProfileDto.FirstName;
            adminProfile.LastName = adminProfileDto.LastName;
            adminProfile.Email = adminProfileDto.Email;
            adminProfile.PhoneNumber = adminProfileDto.PhoneNumber;
            adminProfile.Address = adminProfileDto.Address;
            adminProfile.Bio = adminProfileDto.Bio;
            adminProfile.ImageUrl = string.Join(";", imageUrls);

            await _adminProfileRepository.UpdateAsync(adminProfile);
            await _adminProfileRepository.SaveChangesAsync();

            return true;
        }

        public async Task<bool> DeleteAdminProfileAsync(int id)
        {
            var adminProfile = await _adminProfileRepository.FirstOrDefaultAsync(x => x.Id == id);
            if (adminProfile == null) return false;

            await _adminProfileRepository.RemoveAsync(adminProfile);
            await _adminProfileRepository.SaveChangesAsync();

            return true;
        }
    }
}
