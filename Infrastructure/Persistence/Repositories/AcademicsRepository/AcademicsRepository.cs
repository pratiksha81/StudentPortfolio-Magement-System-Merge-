using Application.Interfaces.Repositories.AcademicsRepository;
using Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.AcademicsRepository
{
	public class AcademicsRepository : Repository<Academics>, IAcademicsRepository
	{
		public AcademicsRepository(ApplicationDbContext context) : base(context)
		{
		}
	}
}
