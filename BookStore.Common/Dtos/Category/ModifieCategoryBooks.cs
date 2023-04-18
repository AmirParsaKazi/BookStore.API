using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Category;

public record ModifieCategoryBooks(string CategoryId, IEnumerable<string> BooksId);
