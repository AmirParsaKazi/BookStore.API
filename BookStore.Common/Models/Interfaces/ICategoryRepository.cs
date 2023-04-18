using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Models.Interfaces;

public interface ICategoryRepository:IGenericRepository<Category>
{
    Task AddCategoryBooksAsync(IEnumerable<CategoryBook> categoryBooks);
    void DeleteCategoryBooks(IEnumerable<CategoryBook> categoryBooks);
}
