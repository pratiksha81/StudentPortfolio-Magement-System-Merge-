using Application.Dto.Certification;
using Application.Interfaces.Services.CertificationService;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Certification.Query
{
    public class CertificationListQuery : IRequest<List<CertificationDto>>
    {

    }
    public class CertificationListQueryQueryHandler : IRequestHandler<CertificationListQuery, List<CertificationDto>>
    {
        private readonly ICertificationService _certificationService;
        public CertificationListQueryQueryHandler(ICertificationService certificationService)
        {
            _certificationService = certificationService;
        }
        public async Task<List<CertificationDto>> Handle(CertificationListQuery request, CancellationToken cancellationToken)
        {
            var response = await _certificationService.GetAllCertificationAsync();
            return response;
        }
    }
}
