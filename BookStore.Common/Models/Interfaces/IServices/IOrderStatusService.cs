using BookStore.Common.Dtos.OrderStatus;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Models.Interfaces.IServices;

public interface IOrderStatusService
{
    Task<string?> CreatOrderStatusAsync(OrderStatusCreate orderStatusCreate);
    Task UpdateOrderStatusAsync(OrderStatusUpdate orderStatusUpdate);
    Task DeleteOrderStatusAsync(OrderStatusDelete orderStatusDelete);
    Task<OrderStatusGet?> GetOrderStatusByIdAsync(string id);
    Task<OrderStatusGetWithoutOrdersInfo?> GetOrderStatusByIdWithoutOrdersInfoAsync(string id);
    Task<IEnumerable<OrderStatusesList>> GetOrderStatusesAsync();
    Task<IEnumerable<OrderStatusesList>> GetOrderStatusesByFilter(OrderStatusesGetByFilter orderStatusesGetByFilter);
}
