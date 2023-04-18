using BookStore.Common.Models;
using BookStore.Common.Models.Interfaces;
using BookStore.Infrastructure.Data;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Infrastructure.Repositories;

public class CategoryRepository : GenericRepository<Category>, ICategoryRepository
{
    public CategoryRepository(ApplicationDBContext context, ILogger logger) : base(context, logger)
    {
    }

    public async Task AddCategoryBooksAsync(IEnumerable<CategoryBook> categoryBooks)
    {
        await _context.CategoryBooks.AddRangeAsync(categoryBooks);
    }

    public void DeleteCategoryBooks(IEnumerable<CategoryBook> categoryBooks)
    {
        _context.CategoryBooks.RemoveRange(categoryBooks);
    }
}
