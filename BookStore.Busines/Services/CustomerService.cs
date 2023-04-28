using AutoMapper;
using BookStore.Busines.Configuration;
using BookStore.Common.Dtos.City;
using BookStore.Common.Dtos.Customer;
using BookStore.Common.Dtos.Discount;
using BookStore.Common.Dtos.Province;
using BookStore.Common.Models;
using BookStore.Common.Models.Interfaces.IServices;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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

    public async Task<CustomerGet?> GetCustomerByIdAsync(string id)
    {
        var customer = await _unitOfWork.Customer.GetByIdAsync(id, p => p.Orders, c => c.City1, q => q.City2);

        if (customer != null)
        {
            //Find and set Age
            if (customer.BirthDate != null)
            {
                var today = DateTime.Today;
                var birthdate = (DateTime)customer.BirthDate;
                var age = today.Year - birthdate.Year;

                // Go back to the year in which the person was born in case of a leap year
                if (birthdate.Date > today.AddYears(-age)) age--;

                customer.Age = age;
            }

            //Set Province For Cities
            if (customer.City1 != null)
            {
                customer.City1 = await _unitOfWork.City.GetByIdAsync(customer.City1.Id, p => p.Province);
            }
            if (customer.City2 != null)
            {
                customer.City2 = await _unitOfWork.City.GetByIdAsync(customer.City2.Id, p => p.Province);
            }

            //Set OrderStatus for each order 
            if (!customer.Orders.IsNullOrEmpty())
            {
                for (int i = 0; i < customer.Orders.Count; i++)
                {
                    customer.Orders[i] = await _unitOfWork.Order.GetByIdAsync(customer.Orders[i].Id, p => p.Staus);
                }
            }


            var customerMapped = _mapper.Map<CustomerGet>(customer);

            return customerMapped;
        }
        return null;
    }

    public async Task<IEnumerable<CustomersList>> GetCustomersAsync()
    {
        var customers = await _unitOfWork.Customer.GetAsync(null, null, null, p => p.City1, q => q.City2);

        foreach (Customer customer in customers)
        {
            if (customer.City1 != null)
            {
                customer.City1 = await _unitOfWork.City.GetByIdAsync(customer.City1.Id, p => p.Province);
            }
            if (customer.City2 != null)
            {
                customer.City2 = await _unitOfWork.City.GetByIdAsync(customer.City2.Id, p => p.Province);
            }
        }

        var customersMapped = _mapper.Map<List<CustomersList>>(customers);
        return customersMapped;
    }

    public async Task<IEnumerable<CustomersList>> GetCustomersByFilter(CustomersGetByFilter customersGetByFilter)
    {
        Func<IQueryable<Customer>, IOrderedQueryable<Customer>>? userOrderType = null;
        if (customersGetByFilter.Order == CustomerOrderBy.Id)
        {
            userOrderType = (a) => a.OrderBy(y => y.Id);
        }
        else if (customersGetByFilter.Order == CustomerOrderBy.FirstName)
        {
            userOrderType = (a) => a.OrderBy(y => y.FirstName);
        }
        else if (customersGetByFilter.Order == CustomerOrderBy.LastName)
        {
            userOrderType = (a) => a.OrderBy(y => y.LastName);
        }
        else if (customersGetByFilter.Order == CustomerOrderBy.Address)
        {
            userOrderType = (a) => a.OrderBy(y => y.Address);
        }
        else if (customersGetByFilter.Order == CustomerOrderBy.BirthDate)
        {
            userOrderType = (a) => a.OrderBy(y => y.BirthDate);
        }
        else if (customersGetByFilter.Order == CustomerOrderBy.Age)
        {
            userOrderType = (a) => a.OrderBy(y => y.Age);
        }
        else if (customersGetByFilter.Order == CustomerOrderBy.Mobile)
        {
            userOrderType = (a) => a.OrderBy(y => y.Mobile);
        }

        Func<IEnumerable<CustomersList>, IOrderedEnumerable<CustomersList>>? orderByCities = null;
        if (customersGetByFilter.Order == CustomerOrderBy.City1)
        {
            userOrderType = (a) => a.OrderBy(y => y.City1);
        }
        else if (customersGetByFilter.Order == CustomerOrderBy.City2)
        {
            userOrderType = (a) => a.OrderBy(y => y.City2);
        }

        string firstName = customersGetByFilter.FirstName;
        Expression<Func<Customer, bool>> filterFirstName = (p) => !firstName.IsNullOrEmpty() ?
             p.FirstName.ToLower().Contains(firstName.Trim().ToLower()) : true;

        string lastName = customersGetByFilter.LastName;
        Expression<Func<Customer, bool>> filterLastName = (p) => !lastName.IsNullOrEmpty() ?
             p.LastName.ToLower().Contains(lastName.Trim().ToLower()) : true;

        string address = customersGetByFilter.Address;
        Expression<Func<Customer, bool>> filterAddress = (p) => !address.IsNullOrEmpty() ?
             p.Address.ToLower().Contains(address.Trim().ToLower()) : true;

        string mobile = customersGetByFilter.Mobile;
        Expression<Func<Customer, bool>> filterMobile = (p) => !mobile.IsNullOrEmpty() ?
             p.Mobile.ToLower().Contains(mobile.Trim().ToLower()) : true;


        AgeFilter age = customersGetByFilter.Age;
        Func<CustomersList, bool> filterAge = (p) => true;
        if (age != null)
        {
            if (age.Min != null && age.Max != null)
            {
                filterAge = (p) => p.Age >= age.Min && p.Age <= age.Max;
            }
        }

        //TODO : Ask how filter date time fields
        BirthDateFilter birthDate = customersGetByFilter.BirthDate;
        Expression<Func<Customer, bool>> filterBirthDate = (p) => true;
        if (birthDate != null)
        {
            if (birthDate.Min != null && birthDate.Max != null)
            {
                filterBirthDate = (p) => p.BirthDate >= birthDate.Min
                          && p.BirthDate <= birthDate.Max;
            }
        }

        var customers = await _unitOfWork.Customer
            .GetFilteredAsync(
            new Expression<Func<Customer, bool>>[]
            {
                filterFirstName,
                filterLastName,
                filterAddress,
                filterMobile,
                filterBirthDate
            },
            userOrderType,
            customersGetByFilter.Skip,
            customersGetByFilter.Take,
             p => p.City1, 
             q => q.City2);

        foreach (Customer customer in customers)
        {
            if (customer.City1 != null)
            {
                customer.City1 = await _unitOfWork.City.GetByIdAsync(customer.City1.Id, p => p.Province);
            }
            if (customer.City2 != null)
            {
                customer.City2 = await _unitOfWork.City.GetByIdAsync(customer.City2.Id, p => p.Province);
            }
        }

        if (customers != null)
        {
            var customersMapped = _mapper.Map<List<CustomersList>>(customers);

            if (orderByCities != null)
            {
                customersMapped = (List<CustomersList>)orderByCities(customersMapped);
            }

            //Find and set Age
            for (int i = 0; i < customersMapped.Count; i++)
            {
                if (customersMapped[i].BirthDate != null)
                {
                    var today = DateTime.Today;
                    var birthdate = (DateTime)customersMapped[i].BirthDate;
                    int customerAge = today.Year - birthdate.Year;

                    // Go back to the year in which the person was born in case of a leap year
                    if (birthdate.Date > today.AddYears(-customerAge)) customerAge--;

                    customersMapped[i].Age = customerAge;
                }
            }

            customersMapped = customersMapped.Where(p => filterAge(p)).ToList();

            return customersMapped;
        }
        return null;
    }


}
