﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Order;

public record ModifieOrderBooks(string OrderId, IEnumerable<string> BooksId);
