using AutoMapper;
using BookStore.Busines.Configuration;
using BookStore.Common.Dtos.Author;
using BookStore.Common.Dtos.Book;
using BookStore.Common.Dtos.Category;
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

public class CategoryService : ICategoryService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CategoryService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    private List<Category> MakeSubCategories(List<CategoryCreate> categories, string parentId)
    {
        List<Category> subCategories = new List<Category>();
        foreach (var category in categories)
        {
            var mappedCategory = _mapper.Map<Category>(category);
            mappedCategory.ParentId = parentId;
            if (!category.SubCategories.IsNullOrEmpty())
            {
                var subCategoriesList = MakeSubCategories((List<CategoryCreate>)category.SubCategories, mappedCategory.Id);
                mappedCategory.Categories.AddRange(subCategoriesList);
            }

            if (!category.Books.IsNullOrEmpty())
            {
                mappedCategory.Books = category.Books
                    .Select(p => new CategoryBook()
                    {
                        BookId = p
                    }).ToList();
            }

            subCategories.Add(mappedCategory);
        }
        return subCategories;
    }

    public async Task<string?> CreatCategoryAsync(CategoryCreate categoryCreate)
    {
        var mappedCategory = _mapper.Map<Category>(categoryCreate);
        mappedCategory.Categories = new List<Category>();
        if (!categoryCreate.SubCategories.IsNullOrEmpty())
        {
            List<Category> subCategories = new List<Category>();
            foreach (var subCategoryCreate in categoryCreate.SubCategories)
            {
                var mappedSubCategory = _mapper.Map<Category>(subCategoryCreate);
                mappedSubCategory.ParentId = mappedCategory.Id;
                mappedSubCategory.Categories = new List<Category>();
                if (!subCategoryCreate.SubCategories.IsNullOrEmpty())
                {
                    var subCategoriesList = MakeSubCategories((List<CategoryCreate>)subCategoryCreate.SubCategories, mappedSubCategory.Id);
                    mappedSubCategory.Categories.AddRange(subCategoriesList);
                }

                if (!subCategoryCreate.Books.IsNullOrEmpty())
                {
                    mappedSubCategory.Books = subCategoryCreate.Books
                        .Select(p => new CategoryBook()
                        {
                            BookId = p
                        }).ToList();
                }

                subCategories.Add(mappedSubCategory);
            }
            mappedCategory.Categories.AddRange(subCategories);

        }

        if (!categoryCreate.Books.IsNullOrEmpty())
        {
            mappedCategory.Books = categoryCreate.Books
                .Select(p => new CategoryBook()
                {
                    BookId = p
                }).ToList();
        }

        var categoryId = await _unitOfWork.Category.Insert(mappedCategory);
        await _unitOfWork.CompleteAsync();
        await _unitOfWork.Dispose();

        return categoryId.ToString();
    }

    private async Task DeleteSubCategories(List<Category> subCategories)
    {
        foreach (var subCategory in subCategories)
        {
            Category? category = await _unitOfWork.
           Category.GetByIdAsync(subCategory.Id,
           p => p.Categories);

            if (!category.Categories.IsNullOrEmpty())
            {
                DeleteSubCategories(category.Categories);
            }
            _unitOfWork.Category.Delete(category);
        }
    }
    public async Task DeleteCategoryAsync(CategoryDelete categoryDelete)
    {
        Category? category = await _unitOfWork.
            Category.GetByIdAsync(categoryDelete.Id,
            p => p.Categories);
        if (!category.Categories.IsNullOrEmpty())
        {
            DeleteSubCategories(category.Categories);
        }

        if (category != null)
        {
            _unitOfWork.Category.Delete(category);

            await _unitOfWork.CompleteAsync();
            await _unitOfWork.Dispose();
        }
    }

    private async Task<IEnumerable<CategoriesList>> MakeCtegoriesTree(List<Category> categories)
    {
        List<CategoriesList> mappedSubCategories = new List<CategoriesList>();
        foreach (var category in categories)
        {
            var subCategoryComplete = await _unitOfWork.Category
                        .GetByIdAsync(category.Id, p => p.Categories);

            var mappedCategory = _mapper.Map<CategoriesList>(subCategoryComplete);
            if (!category.Categories.IsNullOrEmpty())
            {
                mappedCategory.SubCategories = await MakeCtegoriesTree(category.Categories);
            }
            mappedSubCategories.Add(mappedCategory);
        }
        return mappedSubCategories;
    }
    public async Task<IEnumerable<CategoriesList>> GetCategoriesAsync()
    {
        var categories = await _unitOfWork.Category.GetAsync(null, null, null, p => p.Categories);

        List<CategoriesList> categoriesTree = new List<CategoriesList>();
        foreach (var category in categories)
        {
            if (category.ParentId == null)
            {
                var mappedCategory = _mapper.Map<CategoriesList>(category);
                if (!category.Categories.IsNullOrEmpty())
                {
                    mappedCategory.SubCategories = await MakeCtegoriesTree(category.Categories);
                }
                categoriesTree.Add(mappedCategory);
            }
        }

        return categoriesTree;
    }

    public async Task<IEnumerable<CategoriesList>> GetCategoryByFilter(CategoriesGetByFilter categoriesGetByFilter)
    {
        //TODO : ask need this method or not
        throw new NotImplementedException();
    }

    public async Task<CategoryGet?> GetCategoryByIdAsync(string id)
    {
        var category = await _unitOfWork.Category.GetByIdAsync(id, p => p.Categories, q => q.Books);
        if (category != null)
        {
            var categoryMapped = _mapper.Map<CategoryGet>(category);
            if (!category.Categories.IsNullOrEmpty())
            {
                categoryMapped.SubCategories = await MakeCtegoriesTree(category.Categories);
            }

            List<Book> books = new List<Book>();
            for (int i = 0; i < category.Books.Count; i++)
            {
                books.Add(await _unitOfWork.Book.GetByIdAsync(category.Books[i].BookId));
            }


            categoryMapped.Books = _mapper.Map<List<BooksList>>(books);
            return categoryMapped;
        }
        return null;
    }

    public async Task UpdateCategoryAsync(CategoryUpdate categoryUpdate)
    {
        var mappedCategory = _mapper.Map<Category>(categoryUpdate);
        _unitOfWork.Category.Update(mappedCategory);

        await _unitOfWork.CompleteAsync();
        await _unitOfWork.Dispose();
    }

    public async Task AddCategoryBooksAsync(ModifieCategoryBooks modifieCategoryBooks)
    {
        var books = modifieCategoryBooks.BooksId.Select(p => new CategoryBook()
        {
            CategoryId = modifieCategoryBooks.CategoryId,
            BookId = p
        }).ToList();
        await _unitOfWork.Category.AddCategoryBooksAsync(books);

        await _unitOfWork.CompleteAsync();
        await _unitOfWork.Dispose();
    }

    public async Task DeleteCategoryBooks(ModifieCategoryBooks modifieCategoryBooks)
    {
        var books = modifieCategoryBooks.BooksId.Select(p => new CategoryBook()
        {
            CategoryId = modifieCategoryBooks.CategoryId,
            BookId = p
        }).ToList();
        _unitOfWork.Category.DeleteCategoryBooks(books);

        await _unitOfWork.CompleteAsync();
        await _unitOfWork.Dispose();
    }
}
