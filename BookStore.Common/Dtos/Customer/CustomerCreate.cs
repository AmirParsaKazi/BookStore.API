using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Customer;

public record CustomerCreate
    (string FirstName,
    string LastName,
    DateTime? BirthDate,
    string? Address,
    string Mobile,
    string? Image,
    string CityId1,
    String? CityId2
    );
//TODO : Make Order From Create Customer