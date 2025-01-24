using Application.Dto.Certification;
using Application.Interfaces.Services.CertificationService;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Certification.Command
{
    public class UpdateCertificationCommand : CertificationDto, IRequest<bool>
    {
    }

    public class UpdateCertificationCommandHandler : IRequestHandler<UpdateCertificationCommand, bool>
    {
        private readonly ICertificationService _certificationService;

        public UpdateCertificationCommandHandler(ICertificationService certificationService)
        {
            _certificationService = certificationService;
        }

        public async Task<bool> Handle(UpdateCertificationCommand request, CancellationToken cancellationToken)
        {
            var response = await _certificationService.UpdateCertificationAsync(request);
            return response;
        }
    }
}
