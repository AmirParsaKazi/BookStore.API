﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Book;

public record ModifieBookAuthors(string BookId, IEnumerable<string> AuthorsId);
