using Application.Interfaces.Repositories.ExtracurricularActivitiesRepository;
using Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.ExtracurricularActivitiesRepository
{
    public class ExtracurricularActivitiesRepository : Repository<ExtracurricularActivities>, IExtracurricularActivitiesRepository
    {
        public ExtracurricularActivitiesRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
