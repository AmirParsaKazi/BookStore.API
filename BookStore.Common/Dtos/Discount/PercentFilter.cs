using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Discount;

public class PercentFilter : IMinMaxFilterBase<Byte>
{
    public PercentFilter()
    {
        Min = 0;
        Max = 100;
        //TODO : Ask about change default with query on db 
    }
    public Byte Min { get; set; }
    public Byte Max { get; set; }
}
