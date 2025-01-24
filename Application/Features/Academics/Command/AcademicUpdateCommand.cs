using Application.Dto.Academics;
using Application.Interfaces.Services.AcademicsService;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Academics.Command
{
	public class AcademicUpdateCommand : IRequest<AcademicsDto>
	{
		public AcademicsDto UpdateAcademicDto { get; set; }
	}

	public class AcademicUpdateCommandHandler : IRequestHandler<AcademicUpdateCommand, AcademicsDto>
	{
		private readonly IAcademicsService _academicService;

		public AcademicUpdateCommandHandler(IAcademicsService academicService)
		{
			_academicService = academicService;
		}

		public async Task<AcademicsDto> Handle(AcademicUpdateCommand request, CancellationToken cancellationToken)
		{
			// Update academic record using the service
			var response = await _academicService.UpdateAcademics(request.UpdateAcademicDto);
			return response;
		}
	}
}
