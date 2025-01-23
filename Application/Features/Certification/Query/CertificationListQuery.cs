using Application.Dto.Certification;
using Application.Interfaces.Services.CertificationService;
using MediatR;

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
