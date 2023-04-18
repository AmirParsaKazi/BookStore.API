using BookStore.Common.Dtos.Book;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Category; 

public class CategoryGet
{

    public CategoryGet(string Id, string Title, string? ParentId)
    {
        this.Id = Id;
        this.Title = Title;
        this.ParentId = ParentId;
    }

    public string Id { get; set; }
    public string Title { get; set; }
    public string? ParentId { get; set; }
    public IEnumerable<BooksList> Books { get; set; }
    public IEnumerable<CategoriesList> SubCategories { get; set; }


};