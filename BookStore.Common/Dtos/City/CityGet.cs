using BookStore.Common.Dtos.Customer;
using BookStore.Common.Dtos.Province;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.City;

public class CityGet
{
    public CityGet(string Name)
    {
        this.Name = Name;
    }
    public string Name { get; set; }
    public ProvincesList Province { get; set; }
    public IEnumerable<CustomersListWithoutCity> customersList1 { get; set; }
    public IEnumerable<CustomersListWithoutCity> customersList2 { get; set; }
}
