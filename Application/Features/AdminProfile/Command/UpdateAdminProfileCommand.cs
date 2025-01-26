using Application.Dto.AdminProfile;
using Application.Interfaces;
//using Application.Interfaces.Services.AdminProfileService;
using MediatR;

namespace Application.Features.AdminProfile.Command
{
    public class UpdateAdminProfileCommand : IRequest<bool>
    {
        public UpdateAdminProfileDto AdminProfile { get; set; }
    }

    public class UpdateAdminProfileCommandHandler : IRequestHandler<UpdateAdminProfileCommand, bool>
    {
        private readonly IAdminProfileService _adminProfileService;

        public UpdateAdminProfileCommandHandler(IAdminProfileService adminProfileService)
        {
            _adminProfileService = adminProfileService;
        }

        public async Task<bool> Handle(UpdateAdminProfileCommand request, CancellationToken cancellationToken)
        {
            var result = await _adminProfileService.UpdateAdminProfileAsync(request.AdminProfile);
            return result;
        }
    }
}
