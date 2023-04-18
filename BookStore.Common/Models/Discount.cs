using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Models;

public class Discount : BaseEntity
{
    public DateTime StartDate { get; set; } = default!;
    public DateTime? EndDate { get; set; } = default!;
    public byte Percent { get; set; } = default!;

    public Book? Book { get; set; } = default!;
}
