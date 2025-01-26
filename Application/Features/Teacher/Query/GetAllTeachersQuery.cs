using Application.Dto.Teacher;
using Application.Interfaces.Services.TeacherService;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Teacher.Query
{
    public class GetAllTeachersQuery : IRequest<(IQueryable<TeacherDto>,int)>
    {
        public string Qualification { get; set; }
        public string Experience { get; set; }
        public int PageNumber { get; set; } = 1;
        public int PageSize { get; set; } = 5;
    }

    public class GetAllTeachersQueryHandler : IRequestHandler<GetAllTeachersQuery, (IQueryable<TeacherDto>, int)>
    {
        private readonly ITeacherService _teacherService;

        public GetAllTeachersQueryHandler(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public async Task<(IQueryable<TeacherDto>, int)> Handle(GetAllTeachersQuery request, CancellationToken cancellationToken)
        {
            return await _teacherService.GetAllTeachersAsync(request.Qualification, request.Experience, request.PageNumber, request.PageSize);
        }
    }
}
