using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Customer;

public record CustomerUpdate
    (
    string Id,
    string FirstName,
    string LastName,
    DateTime? BirthDate,
    string? Address,
    string Mobile,
    string? Image,
    string CityId1,
    String? CityId2
    );