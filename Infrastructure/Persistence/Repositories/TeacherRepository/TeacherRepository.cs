using Application.Interfaces.Repositories.TeacherRepository;
using Infrastructure.Persistence.Contexts;

namespace Infrastructure.Persistence.Repositories.TeacherRepository
{
    public class TeacherRepository : Repository<Teacher>, ITeacherRepository
    {
        public TeacherRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
