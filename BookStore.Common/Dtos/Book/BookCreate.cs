using BookStore.Common.Dtos.Discount;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Book;
public record BookCreate(string Title,
    string? Summary,
    int Price,
    int Stock,
    string? File,
    byte[]? Image,
    string LanguageId,
    IEnumerable<string>? Authors,
    IEnumerable<string>? Categories,
    DiscountCreateByBook Discount
    );