using AutoMapper;
using BookStore.Busines.Configuration;
using BookStore.Common.Dtos.Customer;
using BookStore.Common.Dtos.Province;
using BookStore.Common.Models;
using BookStore.Common.Models.Interfaces.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Busines.Services;

public class CustomerService : ICustomerService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CustomerService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<string?> CreatCustomerAsync(CustomerCreate customerCreate)
    {
        var mappedCustomer = _mapper.Map<Customer>(customerCreate);

        var customerId = await _unitOfWork.Customer.Insert(mappedCustomer);

        await _unitOfWork.CompleteAsync();
        await _unitOfWork.Dispose();

        return customerId.ToString();
    }

    public async Task DeleteCustomerAsync(CustomerDelete customerDelete)
    {
        Customer? customer = await _unitOfWork.Customer.GetByIdAsync(customerDelete.Id);
        if (customer != null)
        {
            _unitOfWork.Customer.Delete(customer);

            await _unitOfWork.CompleteAsync();
            await _unitOfWork.Dispose();
        }
    }

    public async Task UpdateCustomerAsync(CustomerUpdate customerUpdate)
    {
        var mappedCustomer = _mapper.Map<Customer>(customerUpdate);
        _unitOfWork.Customer.Update(mappedCustomer);

        await _unitOfWork.CompleteAsync();
        await _unitOfWork.Dispose();
    }

    public Task<CustomerGet?> GetCustomerByIdAsync(string id)
    {
        throw new NotImplementedException();
    }

    public async Task<IEnumerable<CustomersList>> GetCustomersAsync()
    {
        var customers = await _unitOfWork.Customer.GetAsync(null, null, null,p=>p.City1,q => q.City2);

        foreach (Customer customer in customers)
        {
            if (customer.City1 != null)
            {
                customer.City1 = await _unitOfWork.City.GetByIdAsync(customer.City1.Id,p=> p.Province);
            }
            if (customer.City2 != null)
            {
                customer.City2 = await _unitOfWork.City.GetByIdAsync(customer.City2.Id, p => p.Province);
            }
        }

        var customersMapped = _mapper.Map<List<CustomersList>>(customers);
        return customersMapped;
    }

    public Task<IEnumerable<CustomersList>> GetCustomersByFilter(CustomersGetByFilter customersGetByFilter)
    {
        throw new NotImplementedException();
    }

   
}
