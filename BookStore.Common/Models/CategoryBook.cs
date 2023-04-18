using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Models;

public class CategoryBook
{
    public string BookId { get; set; } = default!;
    public string CategoryId { get; set; } = default!;

    public Book Book { get; set; } = default!;
    public Category Category { get; set; } = default!;
}
