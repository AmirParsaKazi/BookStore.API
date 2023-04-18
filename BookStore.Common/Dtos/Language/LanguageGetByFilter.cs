using BookStore.Common.Dtos.Author;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Language;

public record LanguageGetByFilter(string? Name, int? Skip, int? Take, LanguageOrderBy? Order);
