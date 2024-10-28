
using Application.Client;
using Application.DTO.Customer.Request;
using Application.DTO.Customer.Response;
using Application.Interface;
using Domain.Exception;
using Outgoing.Http.Refit;
using Refit;
using System.Net;

namespace Outgoing.Http;

public class CustomerClient : ICustomerClient
{
    private readonly ICustomerRefitClient _customertRefitClient;
    private readonly ICustomerMapper _customerMapper;

    public CustomerClient(ICustomerRefitClient customertRefitClient, ICustomerMapper customerMapper)
    {
        _customertRefitClient = customertRefitClient;
        _customerMapper = customerMapper;
    }

    public async Task<CustomerResponse> CreateCustomerAsync(CustomerRequest request)
    {
        var customerDto = await _customertRefitClient.CreateAsync(request);

        return _customerMapper.ToCustomerResponse(customerDto);        
    }

    public async Task<IEnumerable<CustomerResponse>> GetCustomerAllAsync()
    {        
        var customersDto = await _customertRefitClient.GetCustomerAllAsync();

        return customersDto.Select(customer => _customerMapper.ToCustomerResponse(customer));      
    }

    public async Task<CustomerResponse> GetCustomerByIdAsync(Guid id)
    {
        var customerDto = await _customertRefitClient.GetCustomerByIdAsync(id);

        return _customerMapper.ToCustomerResponse(customerDto);        
    }
}
