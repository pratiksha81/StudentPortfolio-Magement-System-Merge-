using Application.Interfaces.Repositories.AdminProfileRepository;
using Domain.Entities;
using Infrastructure.Persistence.Contexts;

namespace Infrastructure.Persistence.Repositories.AdminProfileRepository
{
    public class AdminProfileRepository : Repository<AdminProfile>, IAdminProfileRepository
    {
        public AdminProfileRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
