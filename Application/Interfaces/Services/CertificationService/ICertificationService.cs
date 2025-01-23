using Application.Dto.Certification;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Interfaces.Services.CertificationService
{
    public interface ICertificationService
    {
        Task<List<CertificationDto>> GetAllCertificationAsync();
        Task<AddCertificationDto> AddCertificationAsync(AddCertificationDto request);
    }
}
