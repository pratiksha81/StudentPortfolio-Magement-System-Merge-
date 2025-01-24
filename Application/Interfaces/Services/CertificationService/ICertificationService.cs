using Application.Dto.Certification;
using Application.Dto.Student;

namespace Application.Interfaces.Services.CertificationService
{
    public interface ICertificationService
    {
        Task<List<CertificationDto>> GetAllCertificationAsync();
        Task<AddCertificationDto> AddCertificationAsync(AddCertificationDto request);
        Task<CertificationDto> GetCertificationByIdAsync(int id);
        Task<bool> UpdateCertificationAsync(CertificationDto request);
        Task<int> DeleteCertificationAsync(int id);

        // New method to get certifications by student ID
        Task<List<CertificationDto>> GetCertificationsByStudentIdAsync(int studentId);
    }
}
