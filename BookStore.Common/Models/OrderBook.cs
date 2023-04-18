using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Models;

public class OrderBook
{
    public string OrderId { get; set; } = default!;

    public string BookId { get; set; } = default!;

    public Order Order { get; set; } = default!;
    public Book Book { get; set; } = default!;
}
