using BookStore.Common.Dtos.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Customer;

public record CustomersListWithoutCity
    (
    string Id,
    string FirstName,
    string LastName,
    DateTime? BirthDate,
    string? Address,
    string Mobile,
    string? Image
    );
