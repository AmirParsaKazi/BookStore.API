using BookStore.Common.Dtos.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Customer;

public record CustomersGetByFilter
    (
    string? FirstName,
    string? LastName,
    BirthDateFilter? BirthDate,
    AgeFilter? Age,
    string? Address,
    string? Mobile,
    int? Skip,
    int? Take,
    CustomerOrderBy? Order
    );