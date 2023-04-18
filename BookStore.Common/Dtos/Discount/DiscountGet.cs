using BookStore.Common.Dtos.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Discount;
public class DiscountGet
{
    public DiscountGet(
        DateTime StartDate,
        DateTime? EndDate,
        byte Percent
        )
    {
        this.Percent = Percent;
        this.StartDate = StartDate;
        this.EndDate = EndDate;
    }

    public BooksList Book { get; set; }
    public DateTime StartDate { get; set; }
    public DateTime? EndDate { get; set; }
    public byte Percent { get; set; }
};
