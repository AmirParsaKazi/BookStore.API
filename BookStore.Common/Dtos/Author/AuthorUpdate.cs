﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Author;

public record AuthorUpdate(string Id, string FirstName, string LastName);

