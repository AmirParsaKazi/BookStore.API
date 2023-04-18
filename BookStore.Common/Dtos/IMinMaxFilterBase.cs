using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos;

public interface IMinMaxFilterBase<T>
{
    T Min { get; set; }
    T Max { get; set; }
}
