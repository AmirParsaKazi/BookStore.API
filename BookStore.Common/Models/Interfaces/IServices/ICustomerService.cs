using BookStore.Common.Dtos.Customer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Models.Interfaces.IServices;

public interface ICustomerService
{
    Task<string?> CreatCustomerAsync(CustomerCreate customerCreate);
    Task UpdateCustomerAsync(CustomerUpdate customerUpdate);
    Task DeleteCustomerAsync(CustomerDelete customerDelete);
    Task<CustomerGet?> GetCustomerByIdAsync(string id);
    Task<IEnumerable<CustomersList>> GetCustomersAsync();
    Task<IEnumerable<CustomersList>> GetCustomersByFilter(CustomersGetByFilter customersGetByFilter);
}
