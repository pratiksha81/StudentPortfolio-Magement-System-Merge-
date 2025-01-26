using Application.Dto.AdminProfile;
using Application.Interfaces;
//using Application.Interfaces.Services.AdminProfileService;
using MediatR;

namespace Application.Features.AdminProfile.Command
{
    public class AddAdminProfileCommand : IRequest<int>
    {
        public AddAdminProfileDto AdminProfile { get; set; }
    }

    public class AddAdminProfileCommandHandler : IRequestHandler<AddAdminProfileCommand, int>
    {
        private readonly IAdminProfileService _adminProfileService;

        public AddAdminProfileCommandHandler(IAdminProfileService adminProfileService)
        {
            _adminProfileService = adminProfileService;
        }

        public async Task<int> Handle(AddAdminProfileCommand request, CancellationToken cancellationToken)
        {
            var adminProfileId = await _adminProfileService.AddAdminProfileAsync(request.AdminProfile);
            return adminProfileId;
        }
    }
}
