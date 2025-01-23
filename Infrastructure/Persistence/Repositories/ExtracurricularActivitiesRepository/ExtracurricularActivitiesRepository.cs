using Application.Interfaces.Repositories.ExtracurricularActivitiesRepository;
using Infrastructure.Persistence.Contexts;

namespace Infrastructure.Persistence.Repositories.ExtracurricularActivitiesRepository
{
    public class ExtracurricularActivitiesRepository : Repository<ExtracurricularActivities>, IExtracurricularActivitiesRepository
    {
        public ExtracurricularActivitiesRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
