using AutoMapper;
using BookStore.Busines.Configuration;
using BookStore.Common.Dtos.Author;
using BookStore.Common.Dtos.Book;
using BookStore.Common.Dtos.Category;
using BookStore.Common.Dtos.Discount;
using BookStore.Common.Dtos.Language;
using BookStore.Common.Models;
using BookStore.Common.Models.Interfaces.IServices;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Busines.Services;

public class BookService : IBookService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public BookService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<string?> CreatBookAsync(BookCreate bookCreate)
    {
        var mappedBook = _mapper.Map<Book>(bookCreate);

        if (bookCreate.Authors != null)
        {
            mappedBook.Authors = bookCreate.Authors
                .Select(p => new AuthorBook()
                {
                    AuthorId = p
                }).ToList();
        }

        if (bookCreate.Categories != null)
        {
            mappedBook.Categories = bookCreate.Categories
                .Select(p => new CategoryBook()
                {
                    CategoryId = p
                }).ToList();
        }

        if (bookCreate.Discount != null)
        {
            var discountId = mappedBook.Id;
            mappedBook.Discount = new Discount()
            {
                Id = discountId,
                StartDate = bookCreate.Discount.StartDate,
                EndDate = bookCreate.Discount.EndDate,
                Percent = bookCreate.Discount.Percent,
            };
        }

        var language = await _unitOfWork.Language
            .GetByIdAsync(bookCreate.LanguageId);
        if (language == null)
        {
            return null;
        }
        mappedBook.Language = language;

        var bookId = await _unitOfWork.Book.Insert(mappedBook);

        await _unitOfWork.CompleteAsync();
        await _unitOfWork.Dispose();

        return bookId.ToString();
    }

    public async Task DeleteBookAsync(BookDelete bookDelete)
    {
        Book? book = await _unitOfWork.Book.GetByIdAsync(bookDelete.Id);
        if (book != null)
        {
            _unitOfWork.Book.Delete(book);

            await _unitOfWork.CompleteAsync();
            await _unitOfWork.Dispose();
        }
    }

    public async Task UpdateBookAsync(BookUpdate bookUpdate)
    {
        var mappedBook = _mapper.Map<Book>(bookUpdate);

        if (!bookUpdate.LanguageId.IsNullOrEmpty())
        {
            var language = await _unitOfWork.Language.GetByIdAsync(bookUpdate.LanguageId);
            if (language != null)
            {
                mappedBook.Language = language;
            }
        }

        _unitOfWork.Book.Update(mappedBook);

        await _unitOfWork.CompleteAsync();
        await _unitOfWork.Dispose();
    }

    public async Task<BookGet?> GetBookByIdAsync(string id)
    {
        var book = await _unitOfWork.Book
            .GetByIdAsync(
            id,
            a => a.Authors,
            c => c.Categories,
            d => d.Discount,
            l => l.Language,
            o => o.Orders
            );

        List<Author> authors = new List<Author>();
        if (!book.Authors.IsNullOrEmpty())
        {
            foreach (var author in book.Authors)
            {
                authors.Add(await _unitOfWork.Author.GetByIdAsync(author.AuthorId));
            }
        }

        List<Category> categories = new List<Category>();
        if (!book.Categories.IsNullOrEmpty())
        {
            foreach (var category in book.Categories)
            {
                categories.Add(await _unitOfWork.Category.GetByIdAsync(category.CategoryId));
            }
        }



        if (book != null)
        {
            var bookMapped = _mapper.Map<BookGet>(book);
            bookMapped.Authors = _mapper.Map<List<AuthorsList>>(authors);
            bookMapped.Categories = _mapper.Map<List<CategoryListView>>(categories);
            bookMapped.Language = _mapper.Map<LanguageList>(book.Language);
            bookMapped.Discount = _mapper.Map<DiscountsList>(book.Discount);

            return bookMapped;
        }
        return null;
    }

    public async Task<IEnumerable<BooksList>> GetBooksAsync()
    {
        var books = await _unitOfWork.Book.GetAsync(null, null, null, p => p.Language);
        var booksMapped = _mapper.Map<List<BooksList>>(books);
        var arrBooks = books.ToArray();
        for (int i = 0; i < booksMapped.Count; i++)
        {
            var language = _mapper.Map<LanguageList>(arrBooks[i].Language);
            booksMapped[i].Language = language.Name;
        }

        return booksMapped;
    }

    public async Task<IEnumerable<BooksList>> GetBooksByFilter(BooksGetByFilter booksGetByFilter)
    {
        Func<IQueryable<Book>, IOrderedQueryable<Book>>? userOrderType = null;
        if (booksGetByFilter.Order == BookOrderBy.Id)
        {
            userOrderType = (a) => a.OrderBy(y => y.Id);
        }
        else if (booksGetByFilter.Order == BookOrderBy.Title)
        {
            userOrderType = (a) => a.OrderBy(y => y.Title);
        }
        else if (booksGetByFilter.Order == BookOrderBy.Summary)
        {
            userOrderType = (a) => a.OrderBy(y => y.Summary);
        }
        else if (booksGetByFilter.Order == BookOrderBy.Price)
        {
            userOrderType = (a) => a.OrderBy(y => y.Price);
        }
        else if (booksGetByFilter.Order == BookOrderBy.Stock)
        {
            userOrderType = (a) => a.OrderBy(y => y.Stock);
        }

        Func<IEnumerable<BooksList>, IOrderedEnumerable<BooksList>>? orderByLanguage = null;
        if (booksGetByFilter.Order == BookOrderBy.Language)
        {
            orderByLanguage = (a) => a.OrderBy(y => y.Language);
        }


        string title = booksGetByFilter.Title;
        Expression<Func<Book, bool>> filterTitle = (p) => !title.IsNullOrEmpty() ?
             p.Title.ToLower().Contains(title.Trim().ToLower()) : true;

        string summary = booksGetByFilter.Summary;
        Expression<Func<Book, bool>> filterSummary = (p) => !summary.IsNullOrEmpty() ?
             p.Summary.ToLower().Contains(summary.Trim().ToLower()) : true;

        PriceFilter price = booksGetByFilter.Price;
        Expression<Func<Book, bool>> filterPrice = (p) => true;
        if (price != null)
        {
            filterPrice = (p) => p.Price >= price.Min && p.Price <= price.Max;
        }

        StockFilter stock = booksGetByFilter.Stock;
        Expression<Func<Book, bool>> filterStock = (p) => true;
        if (stock != null)
        {
            filterStock = (p) => p.Stock >= stock.Min && p.Stock <= stock.Max;
        }

        var books = await _unitOfWork.Book
            .GetFilteredAsync(
            new Expression<Func<Book, bool>>[]
            {
                filterTitle,filterSummary,filterPrice,filterStock
            },
            userOrderType,
            booksGetByFilter.Skip,
            booksGetByFilter.Take,
            p => p.Language);

        if (books != null)
        {
            var booksMapped = _mapper.Map<List<BooksList>>(books);

            var arrBooks = books.ToArray();
            for (int i = 0; i < booksMapped.Count; i++)
            {
                var language = _mapper.Map<LanguageList>(arrBooks[i].Language);
                booksMapped[i].Language = language.Name;
            }

            if (orderByLanguage != null)
            {
                orderByLanguage(booksMapped);
            }

            return booksMapped;
        }
        return null;
    }



    public async Task AddBookCategoriesAsync(ModifieBookCategories modifieBookCategories)
    {
        var categories = modifieBookCategories.CategoriesId.Select(p => new CategoryBook()
        {
            BookId = modifieBookCategories.BookId,
            CategoryId = p
        }).ToList();

        await _unitOfWork.Category.AddCategoryBooksAsync(categories);

        await _unitOfWork.CompleteAsync();
        await _unitOfWork.Dispose();
    }
    public async Task DeleteBookCategoriesAsync(ModifieBookCategories modifieBookCategories)
    {
        var categories = modifieBookCategories.CategoriesId.Select(p => new CategoryBook()
        {
            BookId = modifieBookCategories.BookId,
            CategoryId = p
        }).ToList();

        _unitOfWork.Category.DeleteCategoryBooks(categories);

        await _unitOfWork.CompleteAsync();
        await _unitOfWork.Dispose();
    }

    public async Task AddBookAuthorsAsync(ModifieBookAuthors modifieBookAuthors)
    {
        var authors = modifieBookAuthors.AuthorsId.Select(p => new AuthorBook()
        {
            BookId = modifieBookAuthors.BookId,
            AuthorId = p
        }).ToList();
        await _unitOfWork.Author.AddAuthorBooksAsync(authors);

        await _unitOfWork.CompleteAsync();
        await _unitOfWork.Dispose();
    }
    public async Task DeleteBookAuthorsAsync(ModifieBookAuthors modifieBookAuthors)
    {
        var authors = modifieBookAuthors.AuthorsId.Select(p => new AuthorBook()
        {
            BookId = modifieBookAuthors.BookId,
            AuthorId = p
        }).ToList();
        _unitOfWork.Author.DeleteAuthorBooks(authors);

        await _unitOfWork.CompleteAsync();
        await _unitOfWork.Dispose();
    }
}
