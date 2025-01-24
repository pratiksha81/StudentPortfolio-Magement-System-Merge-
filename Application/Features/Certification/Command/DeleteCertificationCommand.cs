using Application.Interfaces.Services.CertificationService;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Certification.Command
{
    public class DeleteCertificationCommand : IRequest<int>
    {
        public int Id { get; set; }
    }

    public class DeleteCertificationCommandHandler : IRequestHandler<DeleteCertificationCommand, int>
    {
        private readonly ICertificationService _certificationService;

        public DeleteCertificationCommandHandler(ICertificationService certificationService)
        {
            _certificationService = certificationService;
        }

        public async Task<int> Handle(DeleteCertificationCommand request, CancellationToken cancellationToken)
        {
            var response = await _certificationService.DeleteCertificationAsync(request.Id);
            return response;
        }
    }
}
