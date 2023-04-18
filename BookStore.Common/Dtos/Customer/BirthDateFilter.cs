using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Customer;

public class BirthDateFilter : IMinMaxFilterBase<DateTime>
{
    public DateTime Min { get; set; }
    public DateTime Max { get; set; }
}
