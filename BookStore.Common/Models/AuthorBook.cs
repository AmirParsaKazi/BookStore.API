using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Models;

public class AuthorBook
{
    public string BookId { get; set; } = default!;
    public string AuthorId { get; set; } = default!;

    public Book Book { get; set; } = default!;
    public Author Author { get; set; } = default!;
}
