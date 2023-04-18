using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Models;

public class Order : BaseEntity
{
    public string CustomerId { get; set; } = default!;
    public int TotalPurchasePrice { get; set; } = default!;
    public DateTime RegistrationDate { get; set; } = default!;
    public string? DispatchNumber { get; set; }
    public string OrderStausId { get; set; } = default!;

    public OrderStatus Staus { get; set; } = default!;
    public Customer Customer { get; set; } = default!;
    public List<OrderBook> Books { get; set; } = default!;
}
