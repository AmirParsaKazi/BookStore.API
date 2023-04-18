using BookStore.Common.Dtos;
using BookStore.Common.Dtos.Author;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Models.Interfaces;

public interface IAuthorRepository : IGenericRepository<Author>
{
    Task AddAuthorBooksAsync(IEnumerable<AuthorBook> authorBooks);
    void DeleteAuthorBooks(IEnumerable<AuthorBook> authorBooks);
}
