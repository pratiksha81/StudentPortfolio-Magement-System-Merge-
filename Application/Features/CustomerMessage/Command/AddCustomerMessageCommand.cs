using Application.Dto.CustomerMessage;
using Application.Interfaces.Services.CustomerMessageService;
using MediatR;

namespace Application.Features.CustomerMessage.Command
{
    public class AddCustomerMessageCommand : CustomerMessageDto, IRequest<CustomerMessageDto>
    {
    }

    public class AddCustomerMessageCommandHandler : IRequestHandler<AddCustomerMessageCommand, CustomerMessageDto>
    {
        private readonly ICustomerMessageService _customerMessageService;

        public AddCustomerMessageCommandHandler(ICustomerMessageService customerMessageService)
        {
            _customerMessageService = customerMessageService;
        }

        public async Task<CustomerMessageDto> Handle(AddCustomerMessageCommand request, CancellationToken cancellationToken)
        {
            var response = await _customerMessageService.SaveCustomerMessageAsync(request);
            return response;
        }
    }
}
