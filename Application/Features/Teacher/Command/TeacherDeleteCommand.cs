using Application.Interfaces.Services.TeacherService;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Teacher.Command
{
    public class TeacherDeleteCommand : IRequest<bool>
    {
        public int TeacherId { get; set; }
    }

    public class TeacherDeleteCommandHandler : IRequestHandler<TeacherDeleteCommand, bool>
    {
        private readonly ITeacherService _teacherService;

        public TeacherDeleteCommandHandler(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public async Task<bool> Handle(TeacherDeleteCommand request, CancellationToken cancellationToken)
        {
            // Delete teacher using the service
            var result = await _teacherService.DeleteTeacher(request.TeacherId);
            return result;
        }
    }
}
