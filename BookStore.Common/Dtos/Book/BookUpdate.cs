using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Book;

public record BookUpdate(string Id, string Title, string? Summary, int Price, int Stock, string? File, byte[]? Image, string LanguageId);
