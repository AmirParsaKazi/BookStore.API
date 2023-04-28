using BookStore.Common.Dtos.Customer;
using BookStore.Common.Dtos.OrderStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Order;

public record OrdersListForOrderStatus
    (
    string Id,
    int TotalPurchasePrice,
    DateTime RegistrationDate,
    string? DispatchNumber
    );
