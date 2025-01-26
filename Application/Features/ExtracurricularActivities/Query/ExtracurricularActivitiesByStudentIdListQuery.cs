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
     public class ExtracurricularActivitiesByStudentIdListQuery : IRequest<List<ExtracurricularActivitiesDto>>
    {
        public int StudentId { get; set; }
    }

    public class ExtracurricularActivitiesByStudentIdListQueryHandler : IRequestHandler<ExtracurricularActivitiesByStudentIdListQuery, List<ExtracurricularActivitiesDto>>
    {
        private readonly IExtracurricularActivitiesService _ecaService;

        public ExtracurricularActivitiesByStudentIdListQueryHandler(IExtracurricularActivitiesService ecaService)
        {
            _ecaService = ecaService;
        }

        public async Task<List<ExtracurricularActivitiesDto>> Handle(ExtracurricularActivitiesByStudentIdListQuery request, CancellationToken cancellationToken)
        {
            var response = await _ecaService.GetExtracurricularActivitiesByStudentIdAsync(request.StudentId);
            return response;
        }

        
    }
}
