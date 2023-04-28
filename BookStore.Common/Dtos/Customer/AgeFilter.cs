using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Customer;

public class AgeFilter : IMinMaxFilterBase<int>
{
    public AgeFilter()
    {
        Min = 0;
        Max = 200;
    }
    public int Min { get; set; }
    public int Max { get; set; }
}
