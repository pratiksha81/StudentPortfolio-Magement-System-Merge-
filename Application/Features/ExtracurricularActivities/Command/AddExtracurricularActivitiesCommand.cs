using Application.Dto.ECA;
using Application.Interfaces.Services.ExtracurricularActivitiesService;
using MediatR;

namespace Application.Features.ExtracurricularActivities.Command
{
    public class AddExtracurricularActivitiesCommand : AddExtracurricularActivitiesDto, IRequest<AddExtracurricularActivitiesDto>
    {
    }

    public class AddExtracurricularActivitiesCommandHandler : IRequestHandler<AddExtracurricularActivitiesCommand, AddExtracurricularActivitiesDto>
    {
        private readonly IExtracurricularActivitiesService _service;

        public AddExtracurricularActivitiesCommandHandler(IExtracurricularActivitiesService service)
        {
            _service = service;
        }

        public async Task<AddExtracurricularActivitiesDto> Handle(AddExtracurricularActivitiesCommand request, CancellationToken cancellationToken)
        {
            var response = await _service.AddExtracurricularActivitiesAsync(request);
            return response;
        }
    }
}
