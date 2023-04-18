using BookStore.Common.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Order;

public record OrderCreate
    (
    string CustomerId,
    int TotalPurchasePrice,
    DateTime RegistrationDate,
    string? DispatchNumber,
    string StausId,
    IEnumerable<string> BooksId
    );
