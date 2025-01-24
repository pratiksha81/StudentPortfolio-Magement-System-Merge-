using Application.Dto.Academics;
using Application.Interfaces.Services.AcademicsService;
using MediatR;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Features.Academics.Query
{
    // Define the query to get all academics
    public class GetAllAcademicsQuery : IRequest<IQueryable<AcademicsDto>>
    {
        public double? GPA { get; set; }
        public string Section { get; set; }
    }

    // Handle the query
    public class GetAllAcademicsQueryHandler : IRequestHandler<GetAllAcademicsQuery, IQueryable<AcademicsDto>>
    {
        private readonly IAcademicsService _academicsService;

        public GetAllAcademicsQueryHandler(IAcademicsService academicsService)
        {
            _academicsService = academicsService;
        }

        public async Task<IQueryable<AcademicsDto>> Handle(GetAllAcademicsQuery request, CancellationToken cancellationToken)
        {
            return await _academicsService.GetAllAcademicsAsync(request.GPA, request.Section);
        }
    }
}
