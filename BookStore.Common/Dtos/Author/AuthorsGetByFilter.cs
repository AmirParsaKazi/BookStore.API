using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Author;

public record AuthorsGetByFilter(string? FirstName, string? LastName, int? Skip, int? Take, AuthorOrderBy? Order);