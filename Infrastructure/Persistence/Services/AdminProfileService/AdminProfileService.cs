using Application.Dto.AdminProfile;
using Application.Interfaces;
using Application.Interfaces.Repositories.AdminProfileRepository;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using System.Linq;

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

        public async Task<AddAdminProfileDto> AddAdminProfileAsync(AddAdminProfileDto adminProfileDto)
        {
            var imageUrls = await HandleImagesAsync(adminProfileDto.Image);

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

            await _adminProfileRepository.AddAsync(adminProfile);
            await _adminProfileRepository.SaveChangesAsync();

            return adminProfileDto;
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
            var adminProfile = await _adminProfileRepository.FirstOrDefaultAsync(x => x.Id == adminProfileDto.Id);
            if (adminProfile == null) return false;

            var imageUrls = await HandleImagesAsync(adminProfileDto.Image);

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

        private async Task<List<string>> HandleImagesAsync(IEnumerable<IFormFile> images)
        {
            var imageUrls = new List<string>();

            if (images != null && images.Any())
            {
                foreach (var image in images)
                {
                    // Validate image size and format
                    if (image.Length > 2 * 1024 * 1024) throw new Exception("Image size exceeds 2 MB.");
                    var extension = Path.GetExtension(image.FileName).ToLower();
                    if (!new[] { ".jpg", ".jpeg", ".png" }.Contains(extension)) throw new Exception("Supported formats are .jpg, .jpeg, and .png.");

                    // Define the upload folder
                    var uploadsFolder = Path.Combine(_hostingEnvironment.WebRootPath, "uploads");
                    if (!Directory.Exists(uploadsFolder)) Directory.CreateDirectory(uploadsFolder);

                    // Save the image with a unique name
                    var fileName = $"{Guid.NewGuid()}{extension}";
                    var filePath = Path.Combine(uploadsFolder, fileName);

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await image.CopyToAsync(stream);
                    }

                    // Add relative URL
                    imageUrls.Add(Path.Combine("uploads", fileName));
                }
            }

            return imageUrls;
        }
    }
}
