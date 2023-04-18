using BookStore.Common.Dtos.Author;
using BookStore.Common.Dtos.Category;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Models.Interfaces.IServices;

public interface ICategoryService
{
    Task<string?> CreatCategoryAsync(CategoryCreate categoryCreate);
    Task UpdateCategoryAsync(CategoryUpdate categoryUpdate);
    Task DeleteCategoryAsync(CategoryDelete categoryDelete);

    Task AddCategoryBooksAsync(ModifieCategoryBooks modifieCategoryBooks);
    Task DeleteCategoryBooks(ModifieCategoryBooks modifieCategoryBooks);

    Task<CategoryGet?> GetCategoryByIdAsync(string id);
    Task<IEnumerable<CategoriesList>> GetCategoriesAsync();
    Task<IEnumerable<CategoriesList>> GetCategoryByFilter(CategoriesGetByFilter categoriesGetByFilter);
}
