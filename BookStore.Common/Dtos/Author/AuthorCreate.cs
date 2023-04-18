using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Author;

public record AuthorCreate(string FirstName, string LastName, IEnumerable<string>? Books);
