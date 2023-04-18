using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Models.Interfaces;

public interface IBookRepository:IGenericRepository<Book>
{

    Task AddBookCategoriesAsync(IEnumerable<CategoryBook> categoryBooks);
    void DeleteBookCategories(IEnumerable<CategoryBook> categoryBooks);

    Task AddAuthorBooksAsync(IEnumerable<AuthorBook> authorBooks);
    void DeleteAuthorBooks(IEnumerable<AuthorBook> authorBooks);
}
