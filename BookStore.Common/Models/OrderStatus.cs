using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Models;

public class OrderStatus : BaseEntity
{
    public string Name { get; set; } = default!;

    public List<Order> Orders { get; set; } = default!;
}
