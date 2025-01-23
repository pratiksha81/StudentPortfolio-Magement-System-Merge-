
using Application.Dto.Certification;
using Application.Dto.ECA;
using Application.Interfaces.Services.CertificationService;
using Application.Interfaces.Services.ExtracurricularActivitiesService;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
