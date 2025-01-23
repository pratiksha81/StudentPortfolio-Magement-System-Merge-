using Application.Dto.Certification;
using Application.Interfaces.Services.CertificationService;
using MediatR;

namespace Application.Features.Certification.Command
{
    public class AddCertificationCommand : AddCertificationDto, IRequest<AddCertificationDto>
    {
    }

    public class AddCertificationCommandHandler : IRequestHandler<AddCertificationCommand, AddCertificationDto>
    {
        private readonly ICertificationService _certification;

        public AddCertificationCommandHandler(ICertificationService _certificationService)
        {
            _certification = _certificationService;
        }

        public async Task<AddCertificationDto> Handle(AddCertificationCommand request, CancellationToken cancellationToken)
        {
            var response = await _certification.AddCertificationAsync(request);
            return response;
        }
    }
}
