using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Order;

public class RegistrationDateFilter : IMinMaxFilterBase<DateTime>
{
    public DateTime Min { get; set; }
    public DateTime Max { get; set; }
}
