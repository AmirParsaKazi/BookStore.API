using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Discount;

public record DiscountsList(
    string Id,
    DateTime StartDate,
    DateTime? EndDate,
    byte Percent
    );
