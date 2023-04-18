using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Models;

public class Province : BaseEntity
{
    public string Name { get; set; } = default!;

    public List<City> Cities { get; set; } = default!;
}
