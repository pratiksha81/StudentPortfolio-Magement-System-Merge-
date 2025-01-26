using System.Threading.Tasks;
using System.Linq;
using Application.Dto.AdminProfile;

namespace Application.Interfaces
{
    public interface IAdminProfileService
    {
        Task<(IQueryable<AdminProfileDto> adminProfiles, int totalCount)> GetAllAdminProfilesAsync(
            string name = null,
            string email = null,
            int pageNumber = 1,
            int pageSize = 5);

        Task<AdminProfileDto> GetAdminProfileByIdAsync(int id);

        Task<int> AddAdminProfileAsync(AddAdminProfileDto adminProfileDto);

        Task<bool> UpdateAdminProfileAsync(UpdateAdminProfileDto adminProfileDto);

        Task<bool> DeleteAdminProfileAsync(int id);
    }
}
