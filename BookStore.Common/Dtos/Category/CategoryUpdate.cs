using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Category;

public record CategoryUpdate(string Id, string? Title, string? ParentId);
