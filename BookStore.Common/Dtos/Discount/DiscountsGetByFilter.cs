using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Discount;

public record DiscountsGetByFilter(
    StartDateFilter? StartDate,
    EndDateFilter? EndDate,
    PercentFilter? Percent,
    int? Skip,
    int? Take,
    DiscountOrderBy? Order
    );
