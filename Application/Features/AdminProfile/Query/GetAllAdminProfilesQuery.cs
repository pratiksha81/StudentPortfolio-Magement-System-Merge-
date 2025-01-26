using Application.Dto.AdminProfile;
using Application.Interfaces;
//using Application.Interfaces.Services.AdminProfileService;
using MediatR;
using System.Linq;

namespace Application.Features.AdminProfile.Query
{
    public class GetAllAdminProfilesQuery : IRequest<(IQueryable<AdminProfileDto>, int)>
    {
        public string Name { get; set; }
        public string Email { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }

    public class GetAllAdminProfilesQueryHandler : IRequestHandler<GetAllAdminProfilesQuery, (IQueryable<AdminProfileDto>, int)>
    {
        private readonly IAdminProfileService _adminProfileService;

        public GetAllAdminProfilesQueryHandler(IAdminProfileService adminProfileService)
        {
            _adminProfileService = adminProfileService;
        }

        public async Task<(IQueryable<AdminProfileDto>, int)> Handle(GetAllAdminProfilesQuery request, CancellationToken cancellationToken)
        {
            return await _adminProfileService.GetAllAdminProfilesAsync(request.Name, request.Email, request.PageNumber, request.PageSize);
        }
    }
}
