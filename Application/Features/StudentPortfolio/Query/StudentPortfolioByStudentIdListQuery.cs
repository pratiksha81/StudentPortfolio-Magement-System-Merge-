using Application.Dto.ECA;
using Application.Dto.StudentPortfolio;
using Application.Interfaces.Services.ExtracurricularActivitiesService;
using Application.Interfaces.Services.StudentPortfolioService;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.StudentPortfolio.Query
{
    
     public class StudentPortfolioByStudentIdListQuery : IRequest<List<StudentPortfolioDto>>
    {
        public int StudentId { get; set; }
    }

    public class StudentPortfolioByStudentIdListQueryHandler : IRequestHandler<StudentPortfolioByStudentIdListQuery, List<StudentPortfolioDto>>
    {
        private readonly IStudentPortfolioService _studentPortfolioService;

        public StudentPortfolioByStudentIdListQueryHandler(IStudentPortfolioService StudentPortfolioService)
        {
            _studentPortfolioService = StudentPortfolioService;
        }

        public async Task<List<StudentPortfolioDto>> Handle(StudentPortfolioByStudentIdListQuery request, CancellationToken cancellationToken)
        {
            var response = await _studentPortfolioService.GetStudentPortfolioAsync(request.StudentId);
            return new List<StudentPortfolioDto> { response };
        }
    }
}
