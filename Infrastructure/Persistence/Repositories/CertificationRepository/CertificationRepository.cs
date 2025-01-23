using Application.Interfaces.Repositories.AppointmentRepository;
using Application.Interfaces.Repositories.CertificationRepository;
using Infrastructure.Persistence.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Persistence.Repositories.CertificationRepository
{
    public class CertificationRepository : Repository<Certification>, ICertificationRepository
    {
        public CertificationRepository(ApplicationDbContext context) : base(context)
        {

        }
    }
}
