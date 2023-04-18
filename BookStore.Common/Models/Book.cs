using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Models;

public class Book : BaseEntity
{
    public string Title { get; set; } = default!;
    public string? Summary { get; set; } = default!;
    public string LanguageId { get; set; } = default!;
    public int Price { get; set; } = default!;
    public int Stock { get; set; } = default!;
    public string? File { get; set; } = default!;
    public byte[]? Image { get; set; } = default!;


    public Language Language { get; set; } = default!;
    public List<AuthorBook>? Authors { get; set; } = default!;
    public List<CategoryBook>? Categories { get; set; } = default!;

    public Discount? Discount { get; set; } = default!;
    public List<OrderBook>? Orders { get; set; } = default!;
}
