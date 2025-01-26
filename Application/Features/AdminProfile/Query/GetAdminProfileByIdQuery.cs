using Application.Dto.AdminProfile;
using Application.Interfaces;
//using Application.Interfaces.Services.AdminProfileService;
using MediatR;

namespace Application.Features.AdminProfile.Query
{
    public class GetAdminProfileByIdQuery : IRequest<AdminProfileDto>
    {
        public int Id { get; set; }
    }

    public class GetAdminProfileByIdQueryHandler : IRequestHandler<GetAdminProfileByIdQuery, AdminProfileDto>
    {
        private readonly IAdminProfileService _adminProfileService;

        public GetAdminProfileByIdQueryHandler(IAdminProfileService adminProfileService)
        {
            _adminProfileService = adminProfileService;
        }

        public async Task<AdminProfileDto> Handle(GetAdminProfileByIdQuery request, CancellationToken cancellationToken)
        {
            return await _adminProfileService.GetAdminProfileByIdAsync(request.Id);
        }
    }
}
