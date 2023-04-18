using BookStore.Common.Dtos.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Author;

public class AuthorGet
{
    public AuthorGet(string FirstName,
                     string LastName)
    {
        this.FirstName = FirstName;
        this.LastName = LastName;
    }
    public string FirstName
    {
        get; set;
    }
    public string LastName
    {
        get; set;
    }

    public List<BooksList> Books
    {
        get; set;
    }
}
