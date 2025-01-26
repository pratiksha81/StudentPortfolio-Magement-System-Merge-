using Application.Dto.Teacher;
using Application.Interfaces.Services.TeacherService;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Teacher.Command
{
    public class TeacherUpdateCommand : IRequest<UpdateTeacherDto>
    {
        public UpdateTeacherDto UpdateTeacherDto { get; set; }
    }

    public class TeacherUpdateCommandHandler : IRequestHandler<TeacherUpdateCommand, UpdateTeacherDto>
    {
        private readonly ITeacherService _teacherService;

        public TeacherUpdateCommandHandler(ITeacherService teacherService)
        {
            _teacherService = teacherService;
        }

        public async Task<UpdateTeacherDto> Handle(TeacherUpdateCommand request, CancellationToken cancellationToken)
        {
            // Update teacher using the service
            var response = await _teacherService.UpdateTeacher(request.UpdateTeacherDto);
            return response;
        }
    }
}
