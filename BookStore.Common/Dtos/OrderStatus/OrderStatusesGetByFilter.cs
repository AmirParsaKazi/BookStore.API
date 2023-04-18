﻿using BookStore.Common.Dtos.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.OrderStatus;

public record OrderStatusesGetByFilter(string? Name, int? Skip, int? Take, OrderStatusOrderBy? Order);
