using BookStore.Common.Dtos.Author;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Book;

public class BooksList
{
    public BooksList(string Id, string Title, string? Summary, int Price, int Stock, string? File, byte[]? Image, string Language) 
    { 
        this.Id = Id;
        this.Title = Title;
        this.Summary = Summary;
        this.Price = Price;
        this.Stock = Stock;
        this.Image = Image;
        this.File = File;
        this.Language = Language;

    }

    public string Id { get; set; }
    public string Title { get; set; }
    public string? Summary { get; set; }
    public int Price { get; set; }
    public int Stock { get; set; }
    public string? File { get; set; }
    public byte[]? Image { get; set; }
    public string Language { get; set; }
}