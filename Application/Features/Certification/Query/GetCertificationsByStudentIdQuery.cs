using Application.Dto.Certification;
using Application.Interfaces.Services.CertificationService;
using MediatR;
using System.Collections.Generic;

namespace Application.Features.Certification.Query
{
    public class GetCertificationsByStudentIdQuery : IRequest<List<CertificationDto>>
    {
        public int StudentId { get; set; }
    }

    public class GetCertificationsByStudentIdQueryHandler : IRequestHandler<GetCertificationsByStudentIdQuery, List<CertificationDto>>
    {
        private readonly ICertificationService _certificationService;

        public GetCertificationsByStudentIdQueryHandler(ICertificationService certificationService)
        {
            _certificationService = certificationService;
        }

        public async Task<List<CertificationDto>> Handle(GetCertificationsByStudentIdQuery request, CancellationToken cancellationToken)
        {
            var response = await _certificationService.GetCertificationsByStudentIdAsync(request.StudentId);
            return response;
        }
    }
}
