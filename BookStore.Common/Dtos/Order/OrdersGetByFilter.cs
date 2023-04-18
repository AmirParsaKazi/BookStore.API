using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Order;

public record OrdersGetByFilter
    (
    int? TotalPurchasePrice,
    RegistrationDateFilter? RegistrationDate,
    string? DispatchNumber,
    int? Skip,
    int? Take,
    OrderOrderBy? Order
    );
