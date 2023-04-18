using BookStore.Common.Dtos.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Language;

public class LanguageGet
{
    public LanguageGet(string Name)
    {
        this.Name = Name;
    }

    public string Name
    {
        get; set;
    }

    public List<BooksList> Books
    {
        get; set;
    }
}
