using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Models;

public class Customer : BaseEntity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;
    public DateTime? BirthDate { get; set; }
    public string? Address { get; set; } 
    public string Mobile { get; set; } = default!;
    public string? Image { get; set; }

    //not mapped
    public int Age { get; set; } = default!;

    public string CityId1 { get; set; } = default!;
    public string? CityId2 { get; set; } = default!;
    
   

    public City City1 { get; set; } = default!;
    public City? City2 { get; set; }

    public List<Order> Orders { get; set; } = default!;
}
