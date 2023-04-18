using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Models;

public class City : BaseEntity
{
    public string Name { get; set; } = default!;

    public string ProvinceId { get; set; } = default!;

    public Province Province { get; set; } = default!;

    public List<Customer> Customers1 { get; set; } = default!;
    public List<Customer> Customers2 { get; set; } = default!;
}
