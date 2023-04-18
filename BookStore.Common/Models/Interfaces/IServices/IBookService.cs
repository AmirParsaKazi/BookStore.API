using BookStore.Common.Dtos.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Models.Interfaces.IServices;

public interface IBookService
{
    Task<string?> CreatBookAsync(BookCreate bookCreate);
    Task UpdateBookAsync(BookUpdate bookUpdate);
    Task DeleteBookAsync(BookDelete bookDelete);
    Task<BookGet?> GetBookByIdAsync(string id);
    Task<IEnumerable<BooksList>> GetBooksAsync();
    Task<IEnumerable<BooksList>> GetBooksByFilter(BooksGetByFilter booksGetByFilter);

    Task AddBookCategoriesAsync(ModifieBookCategories modifieBookCategories);
    Task DeleteBookCategoriesAsync(ModifieBookCategories modifieBookCategories);

    Task AddBookAuthorsAsync(ModifieBookAuthors modifieBookAuthors);
    Task DeleteBookAuthorsAsync(ModifieBookAuthors modifieBookAuthors);
}