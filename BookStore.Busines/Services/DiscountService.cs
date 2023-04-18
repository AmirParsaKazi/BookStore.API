using AutoMapper;
using BookStore.Busines.Configuration;
using BookStore.Common.Dtos.Author;
using BookStore.Common.Dtos.Book;
using BookStore.Common.Dtos.Discount;
using BookStore.Common.Dtos.Language;
using BookStore.Common.Models;
using BookStore.Common.Models.Interfaces.IServices;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Busines.Services;

public class DiscountService : IDiscountService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public DiscountService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<string?> CreatDiscountAsync(DiscountCreate discountCreate)
    {
        var mappedDiscount = _mapper.Map<Discount>(discountCreate);

        var discountId = await _unitOfWork.Discount.Insert(mappedDiscount);

        await _unitOfWork.CompleteAsync();
        await _unitOfWork.Dispose();

        return discountId.ToString();
    }

    public async Task DeleteDiscountAsync(DiscountDelete discountDelete)
    {
        Discount? discount = await _unitOfWork.Discount.GetByIdAsync(discountDelete.Id);
        if (discount != null)
        {
            _unitOfWork.Discount.Delete(discount);

            await _unitOfWork.CompleteAsync();
            await _unitOfWork.Dispose();
        }
    }

    public async Task UpdateDiscountAsync(DiscountUpdate discountUpdate)
    {
        var mappedDiscount = _mapper.Map<Discount>(discountUpdate);
        _unitOfWork.Discount.Update(mappedDiscount);

        await _unitOfWork.CompleteAsync();
        await _unitOfWork.Dispose();
    }

    public async Task<DiscountGet?> GetDiscountByIdAsync(string id)
    {
        var discount = await _unitOfWork.Discount.GetByIdAsync(id, p => p.Book);
        Book book = new Book();
        book = await _unitOfWork.Book.GetByIdAsync(discount.Book.Id, p => p.Language);


        if (discount != null)
        {
            var discountMapped = _mapper.Map<DiscountGet>(discount);
            discountMapped.Book = _mapper.Map<BooksList>(book);

            discountMapped.Book.Language = book.Language.Name;

            return discountMapped;
        }
        return null;
    }

    public async Task<IEnumerable<DiscountsList>> GetDiscountsAsync()
    {
        var discounts = await _unitOfWork.Discount.GetAsync(null, null, null);
        var discountsMapped = _mapper.Map<List<DiscountsList>>(discounts);
        return discountsMapped;
    }

    public async Task<IEnumerable<DiscountsList>> GetDiscountsByFilter(DiscountsGetByFilter discountsGetByFilter)
    {
        Func<IQueryable<Discount>, IOrderedQueryable<Discount>>? userOrderType = null;
        if (discountsGetByFilter.Order == DiscountOrderBy.Id)
        {
            userOrderType = (a) => a.OrderBy(y => y.Id);
        }
        else if (discountsGetByFilter.Order == DiscountOrderBy.Percent)
        {
            userOrderType = (a) => a.OrderBy(y => y.Percent);
        }
        else if (discountsGetByFilter.Order == DiscountOrderBy.StartDate)
        {
            userOrderType = (a) => a.OrderBy(y => y.StartDate);
        }
        else if (discountsGetByFilter.Order == DiscountOrderBy.EndDate)
        {
            userOrderType = (a) => a.OrderBy(y => y.EndDate);
        }

        PercentFilter percent  = discountsGetByFilter.Percent;
        Expression<Func<Discount, bool>> filterPercent = (p) => true;
        if (percent != null)
        {
            filterPercent = (p) => p.Percent >= percent.Min 
            && p.Percent <= percent.Max;
        }

        //TODO : Ask how filter date time fields
        StartDateFilter startDate = discountsGetByFilter.StartDate;
        Expression<Func<Discount, bool>> filterStartDate = (p) => true;
        if (startDate != null)
        {
            if (startDate.Min != null && startDate.Max != null)
            {
                filterStartDate = (p) => p.StartDate >= startDate.Min
                          && p.StartDate <= startDate.Max;
            }
        }

        //TODO : Ask how filter date time fields
        EndDateFilter endDate = discountsGetByFilter.EndDate;
        Expression<Func<Discount, bool>> filterEndDate = (p) => true;
        if (endDate != null)
        {
            filterEndDate = (p) => p.EndDate >= endDate.Min
            && p.EndDate <= endDate.Max;
        }

        var discounts = await _unitOfWork.Discount
            .GetFilteredAsync(
            new Expression<Func<Discount, bool>>[]
            {
                filterPercent,filterStartDate,filterEndDate
            },
            userOrderType,
            discountsGetByFilter.Skip,
            discountsGetByFilter.Take);

        if (discounts != null)
        {
            var discountsMapped = _mapper.Map<List<DiscountsList>>(discounts);

            return discountsMapped;
        }
        return null;
    }


}
