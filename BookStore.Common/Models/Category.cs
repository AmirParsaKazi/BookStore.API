using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Models;

public class Category : BaseEntity
{
    public string Title { get; set; }= default!;
    public string? ParentId { get; set; }

    public Category ParentCategory { get; set; } = default!;
    public List<Category>? Categories { get; set; } = default!;

    public List<CategoryBook> Books { get; set; } = default!;
}
