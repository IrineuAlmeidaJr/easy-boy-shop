using Application.DTO.Customer.Response;
using Application.DTO.Customer.Request;

namespace Application.Client;

public interface ICustomerClient
{
    Task<IEnumerable<CustomerResponse>> GetCustomerAllAsync();
    Task<CustomerResponse> GetCustomerByIdAsync(Guid id);
    Task<CustomerResponse> CreateCustomerAsync(CustomerRequest request);
}
