using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Order;

public enum OrderOrderBy
{
    Id,
    TotalPurchasePrice,
    RegistrationDate,
    DispatchNumber,
    Staus,
    Customer
}
