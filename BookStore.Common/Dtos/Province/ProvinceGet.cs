using BookStore.Common.Dtos.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Province;

public class ProvinceGet
{
    public ProvinceGet(string Name)
    {
        this.Name = Name;
    }
    public string Name { get; set; }
    public IEnumerable<CitiesList> Cities { get; set; }
}
