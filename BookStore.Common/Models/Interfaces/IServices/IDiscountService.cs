using BookStore.Common.Dtos.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Models.Interfaces.IServices;
public interface IDiscountService
{
    Task<string?> CreatDiscountAsync(DiscountCreate discountCreate);
    Task UpdateDiscountAsync(DiscountUpdate discountUpdate);
    Task DeleteDiscountAsync(DiscountDelete discountDelete);
    Task<DiscountGet?> GetDiscountByIdAsync(string id);
    Task<IEnumerable<DiscountsList>> GetDiscountsAsync();
    Task<IEnumerable<DiscountsList>> GetDiscountsByFilter(DiscountsGetByFilter discountsGetByFilter);

}
