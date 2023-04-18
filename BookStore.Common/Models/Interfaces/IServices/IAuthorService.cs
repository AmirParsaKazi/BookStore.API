using BookStore.Common.Dtos.Author;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Models.Interfaces.IServices;

public interface IAuthorService
{
    Task<string?> CreatAuthorAsync(AuthorCreate authorCreate);
    Task UpdateAuthorAsync(AuthorUpdate authorUpdate);
    Task DeleteAuthorAsync(AuthorDelete authorDelete);
    Task<AuthorGet?> GetAuthorByIdAsync(string id);
    Task<IEnumerable<AuthorsList>> GetAuthorsAsync();
    Task<IEnumerable<AuthorsList>> GetAuthorsByFilter(AuthorsGetByFilter authorsGetByFilter);
   
    Task AddAuthorBooksAsync(ModifieAuthorBooks modifieAuthorBooks);
    Task DeleteAuthorBooks(ModifieAuthorBooks modifieAuthorBooks);
}
