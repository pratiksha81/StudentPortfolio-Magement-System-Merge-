using Application.Dto.Certification;

namespace Application.Interfaces.Services.CertificationService
{
    public interface ICertificationService
    {
        Task<List<CertificationDto>> GetAllCertificationAsync();
        Task<AddCertificationDto> AddCertificationAsync(AddCertificationDto request);
    }
}
