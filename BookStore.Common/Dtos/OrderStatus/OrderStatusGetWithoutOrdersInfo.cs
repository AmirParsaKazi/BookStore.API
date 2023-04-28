using BookStore.Common.Dtos.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.OrderStatus;

public class OrderStatusGetWithoutOrdersInfo
{
    public OrderStatusGetWithoutOrdersInfo(string Name)
    {
        this.Name = Name;
    }
    public string Name { get; set; }
}
