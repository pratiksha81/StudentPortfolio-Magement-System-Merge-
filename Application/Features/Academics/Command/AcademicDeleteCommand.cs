using Application.Interfaces.Services.AcademicsService;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Features.Academics.Command
{
	public class AcademicDeleteCommand : IRequest<bool>
	{
		public int AcademicId { get; set; }
	}

	public class AcademicDeleteCommandHandler : IRequestHandler<AcademicDeleteCommand, bool>
    {
        private readonly IAcademicsService _academicService;

		public AcademicDeleteCommandHandler(IAcademicsService academicService)
		{
			_academicService = academicService;
		}

		public async Task<bool> Handle(AcademicDeleteCommand request, CancellationToken cancellationToken)
		{
			// Delete academic record using the service
			var result = await _academicService.DeleteAcademics(request.AcademicId);
			return result;
		}
	}
}
