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
    public class GetCertificationByIdQuery : IRequest<CertificationDto>
    {
        public int Id { get; set; }
    }

    public class GetCertificationByIdQueryHandler : IRequestHandler<GetCertificationByIdQuery, CertificationDto>
    {
        private readonly ICertificationService _certificationService;

        public GetCertificationByIdQueryHandler(ICertificationService certificationService)
        {
            _certificationService = certificationService;
        }

        public async Task<CertificationDto> Handle(GetCertificationByIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _certificationService.GetCertificationByIdAsync(request.Id);
            return response;
        }
    }
}
