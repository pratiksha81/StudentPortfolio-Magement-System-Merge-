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
	public class AcademicRegistrationCommand : IRequest<AcademicsDto>
	{
		public AcademicsDto AcademicsDto { get; set; }
	}

	public class AcademicRegistrationCommandHandler : IRequestHandler<AcademicRegistrationCommand, AcademicsDto>
    {
        private readonly IAcademicsService _academicService;

		public AcademicRegistrationCommandHandler(IAcademicsService academicService)
		{
			_academicService = academicService;
		}

		public async Task<AcademicsDto> Handle(AcademicRegistrationCommand request, CancellationToken cancellationToken)
		{
			// Save academic record using the service
			var response = await _academicService.RegisterAcademics(request.AcademicsDto);
			return response;
		}
	}
}
