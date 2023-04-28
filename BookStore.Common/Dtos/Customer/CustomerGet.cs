using BookStore.Common.Dtos.City;
using BookStore.Common.Dtos.Order;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Customer;

public class CustomerGet
{
    public string Id { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public DateTime? BirthDate { get; set; }
    public int? Age { get; set; }
    public string? Address { get; set; }
    public string Mobile { get; set; }
    public string? Image { get; set; }
    public CitiesList City1 { get; set; }
    public CitiesList? City2 { get; set; }

    public IEnumerable<OrdersListForCustomer> Orders { get; set; }
}
