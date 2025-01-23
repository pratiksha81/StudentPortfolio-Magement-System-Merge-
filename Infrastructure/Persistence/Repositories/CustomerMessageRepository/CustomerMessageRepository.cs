using Application.Interfaces.Repositories.CustomerMessageRepository;
using Infrastructure.Persistence.Contexts;

namespace Infrastructure.Persistence.Repositories.CustomerMessageRepository
{
    public class CustomerMessageRepository : Repository<CustomerMessage>, ICustomerMessageRepository
    {
        public CustomerMessageRepository(ApplicationDbContext context) : base(context)
        {
            
        }
    }
}
