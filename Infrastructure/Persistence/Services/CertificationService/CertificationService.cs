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
            _logger = logger;
            _certificationRepository = certificationRepository;
        }

        public async Task<AddCertificationDto> AddCertificationAsync(AddCertificationDto request)
        {
            var NewCertification = new Certification
            {
                Title = request.Title,
                Description = request.Description,  
                ReceivedDate = request.ReceivedDate,
            };
            await _certificationRepository.AddAsync(NewCertification);
            await _certificationRepository.SaveChangesAsync();
            _logger.LogInformation($"{nameof(AddCertificationAsync)} trigger function successfully added a new Certification ");
            return request;
        }
       
        public async Task<List<CertificationDto>> GetAllCertificationAsync()
        {

            _logger.LogInformation($"{nameof(GetAllCertificationAsync)} method triggered");

            var CertificationList = await _certificationRepository.Queryable.ToListAsync();
            var contactDtos = CertificationList.Select(c => new CertificationDto
            {
                Id = c.Id,
                Title = c.Title,
                Description= c.Description, 
                ReceivedDate = c.ReceivedDate,
            }).ToList();

            _logger.LogInformation($"{nameof(GetAllCertificationAsync)} method returned contacts: {JsonConvert.SerializeObject(contactDtos)}");
            return contactDtos;
        }

    }
}
