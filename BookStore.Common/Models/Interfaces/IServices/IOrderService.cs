using BookStore.Common.Dtos.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Models.Interfaces.IServices;
public interface IOrderService 
{
    Task<string?> CreatOrderAsync(OrderCreate orderCreate);
    Task UpdateOrderAsync(OrderUpdate orderUpdate);
    Task DeleteOrderAsync(OrderDelete orderDelete);
    Task<OrderGet?> GetOrderByIdAsync(string id);
    Task<IEnumerable<OrdersList>> GetOrdersAsync();
    Task<IEnumerable<OrdersList>> GetOrdersByFilter(OrdersGetByFilter ordersGetByFilter);
}
