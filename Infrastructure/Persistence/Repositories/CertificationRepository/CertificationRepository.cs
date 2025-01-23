using Application.Interfaces.Repositories.CertificationRepository;
using Infrastructure.Persistence.Contexts;


namespace Infrastructure.Persistence.Repositories.CertificationRepositorys
{
    public class CertificationRepository : Repository<Certification>, ICertificationRepository
    {
     //   private readonly ApplicationDbContext _context;
        private readonly ApplicationDbContext _applicationDbContext;
        public CertificationRepository(ApplicationDbContext dbContext) : base(dbContext)
        {
            this._applicationDbContext = dbContext;
        }
    }

}
