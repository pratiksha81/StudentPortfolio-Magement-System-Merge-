using Application.Interfaces;
//using Application.Interfaces.Services.AdminProfileService;
using MediatR;

namespace Application.Features.AdminProfile.Command
{
    public class DeleteAdminProfileCommand : IRequest<bool>
    {
        public int Id { get; set; }
    }

    public class DeleteAdminProfileCommandHandler : IRequestHandler<DeleteAdminProfileCommand, bool>
    {
        private readonly IAdminProfileService _adminProfileService;

        public DeleteAdminProfileCommandHandler(IAdminProfileService adminProfileService)
        {
            _adminProfileService = adminProfileService;
        }

        public async Task<bool> Handle(DeleteAdminProfileCommand request, CancellationToken cancellationToken)
        {
            var result = await _adminProfileService.DeleteAdminProfileAsync(request.Id);
            return result;
        }
    }
}
