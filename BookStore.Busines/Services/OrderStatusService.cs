using AutoMapper;
using BookStore.Busines.Configuration;
using BookStore.Common.Dtos.City;
using BookStore.Common.Dtos.OrderStatus;
using BookStore.Common.Dtos.Province;
using BookStore.Common.Models;
using BookStore.Common.Models.Interfaces.IServices;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Busines.Services;

public class OrderStatusService : IOrderStatusService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public OrderStatusService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<string?> CreatOrderStatusAsync(OrderStatusCreate orderStatusCreate)
    {
        var mappedOrderStatus = _mapper.Map<OrderStatus>(orderStatusCreate);

        var orderStatusId = await _unitOfWork.OrderStatus.Insert(mappedOrderStatus);

        await _unitOfWork.CompleteAsync();
        await _unitOfWork.Dispose();

        return orderStatusId.ToString();
    }

    public async Task DeleteOrderStatusAsync(OrderStatusDelete orderStatusDelete)
    {
        OrderStatus? orderStatus = await _unitOfWork.OrderStatus.GetByIdAsync(orderStatusDelete.Id);
        if (orderStatus != null)
        {
            _unitOfWork.OrderStatus.Delete(orderStatus);

            await _unitOfWork.CompleteAsync();
            await _unitOfWork.Dispose();
        }
    }

    public async Task UpdateOrderStatusAsync(OrderStatusUpdate orderStatusUpdate)
    {
        var mappedOrderStatus = _mapper.Map<OrderStatus>(orderStatusUpdate);
        _unitOfWork.OrderStatus.Update(mappedOrderStatus);

        await _unitOfWork.CompleteAsync();
        await _unitOfWork.Dispose();
    }

    public async Task<OrderStatusGet?> GetOrderStatusByIdAsync(string id)
    {
        var orderStatus = await _unitOfWork.OrderStatus.GetByIdAsync(id, p => p.Orders);

        if (orderStatus != null)
        {
            var orderStatusMapped = _mapper.Map<OrderStatusGet>(orderStatus);

            return orderStatusMapped;
        }
        return null;
    }

    public async Task<OrderStatusGetWithoutOrdersInfo?> GetOrderStatusByIdWithoutOrdersInfoAsync(string id)
    {
        var orderStatus = await _unitOfWork.OrderStatus.GetByIdAsync(id);

        if (orderStatus != null)
        {
            var orderStatusMapped = _mapper.Map<OrderStatusGetWithoutOrdersInfo>(orderStatus);

            return orderStatusMapped;
        }
        return null;
    }

    public async Task<IEnumerable<OrderStatusesList>> GetOrderStatusesAsync()
    {
        var orderStatuses = await _unitOfWork.OrderStatus.GetAsync(null, null, null);
        var orderStatusesMapped = _mapper.Map<List<OrderStatusesList>>(orderStatuses);
        return orderStatusesMapped;
    }

    public async Task<IEnumerable<OrderStatusesList>> GetOrderStatusesByFilter(OrderStatusesGetByFilter orderStatusesGetByFilter)
    {
        Func<IQueryable<OrderStatus>, IOrderedQueryable<OrderStatus>>? userOrderType = null;
        if (orderStatusesGetByFilter.Order == OrderStatusOrderBy.Id)
        {
            userOrderType = (a) => a.OrderBy(y => y.Id);
        }
        else if (orderStatusesGetByFilter.Order == OrderStatusOrderBy.Name)
        {
            userOrderType = (a) => a.OrderBy(y => y.Name);
        }


        string name = orderStatusesGetByFilter.Name;
        Expression<Func<OrderStatus, bool>> filterName = (p) => !name.IsNullOrEmpty() ?
             p.Name.ToLower().Contains(name.Trim().ToLower()) : true;


        var orderStatus = await _unitOfWork.OrderStatus
            .GetFilteredAsync(
            new Expression<Func<OrderStatus, bool>>[]
            {
                filterName
            },
            userOrderType,
            orderStatusesGetByFilter.Skip,
            orderStatusesGetByFilter.Take);

        if (orderStatus != null)
        {
            var orderStatusMapped = _mapper.Map<List<OrderStatusesList>>(orderStatus);
            return orderStatusMapped;
        }
        return null;
    }


}
