using Application.Dto.Certification;
using Application.Interfaces.Repositories.CertificationRepository;
using Application.Interfaces.Services.CertificationService;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace Infrastructure.Persistence.Services.CertificationService
{
    public class CertificationService : ICertificationService
    {
        private readonly ICertificationRepository _certificationRepository;
        private readonly ILogger<CertificationService> _logger;

        public CertificationService(ICertificationRepository certificationRepository, ILogger<CertificationService> logger)
        {
            _certificationRepository = certificationRepository;
            _logger = logger;
        }

        public async Task<AddCertificationDto> AddCertificationAsync(AddCertificationDto request)
        {
            var newCertification = new Certification
            {
                Title = request.Title,
                Description = request.Description,
                ReceivedDate = request.ReceivedDate,
                StudentId = request.StudentId // Assigning StudentId
            };

            await _certificationRepository.AddAsync(newCertification);
            await _certificationRepository.SaveChangesAsync();
            _logger.LogInformation($"{nameof(AddCertificationAsync)} method successfully added a new Certification with Id: {newCertification.Id}");
            return request;
        }

        public async Task<List<CertificationDto>> GetAllCertificationAsync()
        {
            _logger.LogInformation($"{nameof(GetAllCertificationAsync)} method triggered");

            var certificationList = await _certificationRepository.Queryable.ToListAsync();
            var certificationDtos = certificationList.Select(c => new CertificationDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                ReceivedDate = c.ReceivedDate,
                StudentId = c.StudentId // Include StudentId
            }).ToList();

            _logger.LogInformation($"{nameof(GetAllCertificationAsync)} method returned certifications: {JsonConvert.SerializeObject(certificationDtos)}");
            return certificationDtos;
        }

        public async Task<CertificationDto> GetCertificationByIdAsync(int id)
        {
            _logger.LogInformation($"{nameof(GetCertificationByIdAsync)} method triggered for Id: {id}");

            var certification = await _certificationRepository.Queryable.FirstOrDefaultAsync(c => c.Id == id);
            if (certification == null)
            {
                _logger.LogWarning($"Certification with Id {id} not found");
                return null;
            }

            var certificationDto = new CertificationDto
            {
                Id = certification.Id,
                Title = certification.Title,
                Description = certification.Description,
                ReceivedDate = certification.ReceivedDate,
                StudentId = certification.StudentId // Include StudentId
            };

            _logger.LogInformation($"{nameof(GetCertificationByIdAsync)} method returned: {JsonConvert.SerializeObject(certificationDto)}");
            return certificationDto;
        }

        public async Task<bool> UpdateCertificationAsync(CertificationDto request)
        {
            _logger.LogInformation($"{nameof(UpdateCertificationAsync)} method triggered for Id: {request.Id}");

            var existingCertification = await _certificationRepository.Queryable.FirstOrDefaultAsync(c => c.Id == request.Id);
            if (existingCertification == null)
            {
                _logger.LogWarning($"Certification with Id {request.Id} not found");
                return false;
            }

            existingCertification.Title = request.Title;
            existingCertification.Description = request.Description;
            existingCertification.ReceivedDate = request.ReceivedDate;
            existingCertification.StudentId = request.StudentId; // Update StudentId if needed

            _certificationRepository.Update(existingCertification);
            await _certificationRepository.SaveChangesAsync();

            _logger.LogInformation($"{nameof(UpdateCertificationAsync)} method successfully updated certification with Id: {request.Id}");
            return true;
        }

        public async Task<int> DeleteCertificationAsync(int id)
        {
            _logger.LogInformation($"{nameof(DeleteCertificationAsync)} method triggered for Id: {id}");

            var certificationToDelete = await _certificationRepository.Queryable.FirstOrDefaultAsync(c => c.Id == id);
            if (certificationToDelete == null)
            {
                _logger.LogWarning($"Certification with Id {id} not found");
                return 0;
            }

            _certificationRepository.Remove(certificationToDelete);
            await _certificationRepository.SaveChangesAsync();

            _logger.LogInformation($"{nameof(DeleteCertificationAsync)} method successfully deleted certification with Id: {id}");
            return id;
        }

        public async Task<List<CertificationDto>> GetCertificationsByStudentIdAsync(int studentId)
        {
            _logger.LogInformation($"{nameof(GetCertificationsByStudentIdAsync)} method triggered for StudentId: {studentId}");

            var certifications = await _certificationRepository.Queryable
                .Where(c => c.StudentId == studentId)
                .ToListAsync();

            var certificationDtos = certifications.Select(c => new CertificationDto
            {
                Id = c.Id,
                Title = c.Title,
                Description = c.Description,
                ReceivedDate = c.ReceivedDate,
                StudentId = c.StudentId
            }).ToList();

            _logger.LogInformation($"{nameof(GetCertificationsByStudentIdAsync)} method returned certifications: {JsonConvert.SerializeObject(certificationDtos)}");
            return certificationDtos;
        }
    }
}
