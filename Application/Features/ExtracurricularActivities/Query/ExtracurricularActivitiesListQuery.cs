using Application.Dto.ECA;
using Application.Interfaces.Services.ExtracurricularActivitiesService;
using MediatR;

namespace Application.Features.ExtracurricularActivities.Query
{

    public class ExtracurricularActivitiesListQuery : IRequest<List<ExtracurricularActivitiesDto>>
    {

    }
    public class CertificationListQueryQueryHandler : IRequestHandler<ExtracurricularActivitiesListQuery, List<ExtracurricularActivitiesDto>>
    {
        private readonly IExtracurricularActivitiesService _service;
        public CertificationListQueryQueryHandler(IExtracurricularActivitiesService service)
        {
            _service = service;
        }
        public async Task<List<ExtracurricularActivitiesDto>> Handle(ExtracurricularActivitiesListQuery request, CancellationToken cancellationToken)
        {
            var response = await _service.GetAllExtracurricularActivitiesAsync();
            return response;

        }
    }
}
