using BookStore.Common.Dtos.Author;
using BookStore.Common.Dtos.Category;
using BookStore.Common.Dtos.Language;
using BookStore.Common.Dtos.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Book;
public class BookGet
{
    public BookGet(string Title, string? Summary, int Price, int Stock, string? File, byte[]? Image)
    {
        this.Title = Title;
        this.Summary = Summary;
        this.Price = Price;
        this.Stock = Stock;
        this.File = File;
        this.Image = Image;
    }
    public string Title { get; set; }
    public string? Summary { get; set; }
    public int Price { get; set; }
    public int Stock { get; set; }
    public string? File { get; set; }
    public byte[]? Image { get; set; }

    public LanguageList Language { get; set; }
    public IEnumerable<AuthorsList> Authors { get; set; }
    public IEnumerable<CategoryListView> Categories { get; set; }
    public DiscountsList Discount { get; set; }


    //TODO : ADD Orders on BookGet
    //public IEnumerable<OrderList> Orders { get; set; }

}
