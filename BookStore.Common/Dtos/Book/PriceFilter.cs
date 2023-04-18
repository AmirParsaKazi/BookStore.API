using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Book;

public class PriceFilter : IMinMaxFilterBase<int>
{
    public PriceFilter()
    {
        Min = 0;
        Max = 999999;
        //TODO : Ask about change default with query on db 
    }
    public int Min { get ; set ; }
    public int Max { get; set; }
}
