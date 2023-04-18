using AutoMapper;
using BookStore.Busines.Configuration;
using BookStore.Common.Dtos.Author;
using BookStore.Common.Dtos.Book;
using BookStore.Common.Dtos.Language;
using BookStore.Common.Models;
using BookStore.Common.Models.Interfaces.IServices;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;
using static System.Reflection.Metadata.BlobBuilder;

namespace BookStore.Busines.Services;

public class AuthorService : IAuthorService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public AuthorService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<string?> CreatAuthorAsync(AuthorCreate authorCreate)
    {
        var mappedAuthor = _mapper.Map<Author>(authorCreate);

        if (authorCreate.Books != null)
        {
            mappedAuthor.Books = authorCreate.Books.Select(p => new AuthorBook()
            {
                BookId = p
            }).ToList();
        }

        var authorId = await _unitOfWork.Author.Insert(mappedAuthor);

        await _unitOfWork.CompleteAsync();
        await _unitOfWork.Dispose();

        return authorId.ToString();
    }
    public async Task DeleteAuthorAsync(AuthorDelete authorDelete)
    {
        Author? author = await _unitOfWork.Author.GetByIdAsync(authorDelete.Id);
        if (author != null)
        {
            _unitOfWork.Author.Delete(author);

            await _unitOfWork.CompleteAsync();
            await _unitOfWork.Dispose();
        }
    }
    public async Task UpdateAuthorAsync(AuthorUpdate authorUpdate)
    {

        var mappedAuthor = _mapper.Map<Author>(authorUpdate);
        _unitOfWork.Author.Update(mappedAuthor);

        await _unitOfWork.CompleteAsync();
        await _unitOfWork.Dispose();
    }
    public async Task<AuthorGet?> GetAuthorByIdAsync(string id)
    {
        var author = await _unitOfWork.Author.GetByIdAsync(id, p => p.Books);
        List<Book> books = new List<Book>();
        for (int i = 0; i < author.Books.Count; i++)
        {
            books.Add(await _unitOfWork.Book.GetByIdAsync(author.Books[i].BookId,p => p.Language));
        }

        if (author != null)
        {
            var authorMapped = _mapper.Map<AuthorGet>(author);
            authorMapped.Books = _mapper.Map<List<BooksList>>(books);

            for (int i = 0;i< authorMapped.Books.Count; i++)
            {
                authorMapped.Books[i].Language = books[i].Language.Name;
            }

            return authorMapped;
        }
        return null;
    }
    public async Task<IEnumerable<AuthorsList>> GetAuthorsAsync()
    {
        var authors = await _unitOfWork.Author.GetAsync(null, null, null);
        var authorsMapped = _mapper.Map<List<AuthorsList>>(authors);
        return authorsMapped;
    }

    public async Task<IEnumerable<AuthorsList>> GetAuthorsByFilter(AuthorsGetByFilter authorsGetByFilter)
    {
        Func<IQueryable<Author>, IOrderedQueryable<Author>>? userOrderType = null;
        if (authorsGetByFilter.Order == AuthorOrderBy.Id)
        {
            userOrderType = (a) => a.OrderBy(y => y.Id);
        }
        else if (authorsGetByFilter.Order == AuthorOrderBy.FirstName)
        {
            userOrderType = (a) => a.OrderBy(y => y.FirstName);
        }
        else if (authorsGetByFilter.Order == AuthorOrderBy.LastName)
        {
            userOrderType = (a) => a.OrderBy(y => y.LastName);
        }


        string firstName = authorsGetByFilter.FirstName;
        Expression<Func<Author, bool>> filterFirstName = (p) =>!firstName.IsNullOrEmpty()?
             p.FirstName.ToLower().Contains(firstName.Trim().ToLower()) : true;



        string lastName = authorsGetByFilter.LastName;
        Expression<Func<Author, bool>> filterLastName = (p) => !lastName.IsNullOrEmpty() ?
             p.LastName.ToLower().Contains(lastName.Trim().ToLower()) : true;

        var authors = await _unitOfWork.Author
            .GetFilteredAsync(
            new Expression<Func<Author, bool>>[]
            { 
                filterFirstName,filterLastName
            },
            userOrderType,
            authorsGetByFilter.Skip,
            authorsGetByFilter.Take);

        if (authors != null)
        {
            var authorMapped = _mapper.Map<List<AuthorsList>>(authors);
            return authorMapped;
        }
        return null;
    }

    public async Task AddAuthorBooksAsync(ModifieAuthorBooks modifieAuthorBooks)
    {
        var books = modifieAuthorBooks.BooksId.Select(p => new AuthorBook()
        {
            AuthorId = modifieAuthorBooks.AuthorId,
            BookId = p
        }).ToList();
        await _unitOfWork.Author.AddAuthorBooksAsync(books);

        await _unitOfWork.CompleteAsync();
        await _unitOfWork.Dispose();
    }

    public async Task DeleteAuthorBooks(ModifieAuthorBooks modifieAuthorBooks)
    {
        var books = modifieAuthorBooks.BooksId.Select(p => new AuthorBook()
        {
            AuthorId = modifieAuthorBooks.AuthorId,
            BookId = p
        }).ToList();
        _unitOfWork.Author.DeleteAuthorBooks(books);

        await _unitOfWork.CompleteAsync();
        await _unitOfWork.Dispose();
    }
}
