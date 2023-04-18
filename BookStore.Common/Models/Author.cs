using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Models;

public class Author : BaseEntity
{
    public string FirstName { get; set; } = default!;
    public string LastName { get; set; } = default!;

    public List<AuthorBook> Books { get; set; } = default!;
}
