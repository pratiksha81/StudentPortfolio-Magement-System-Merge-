using Application.Dto.Academics;
using Application.Interfaces.Services.AcademicsService;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Academics.Query
{
	public class GetAcademicByIdQuery : IRequest<AcademicsDto>
	{
		public int Id { get; set; }
    }

	public class GetAcademicByIdQueryHandler : IRequestHandler<GetAcademicByIdQuery, AcademicsDto>
    {
		private readonly IAcademicsService _academicsService;

		public GetAcademicByIdQueryHandler(IAcademicsService academicService)
		{
			_academicsService = academicService;
		}

		public async Task<AcademicsDto> Handle(GetAcademicByIdQuery request, CancellationToken cancellationToken)
		{
			// Call the service method to get the academic record by ID
			return await _academicsService.GetAcademicsByIdAsync(request.Id);
		}
	}
}
