using Application.Dto.CustomerMessage;

namespace Application.Interfaces.Services.CustomerMessageService
{
    public interface ICustomerMessageService
    {
        Task<CustomerMessageDto> SaveCustomerMessageAsync(CustomerMessageDto message);
    }
}
