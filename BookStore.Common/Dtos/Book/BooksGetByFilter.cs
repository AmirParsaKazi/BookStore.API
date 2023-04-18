using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Book;

public record BooksGetByFilter(string? Title, string? Summary, PriceFilter? Price, StockFilter? Stock, int? Skip, int? Take, BookOrderBy? Order);
