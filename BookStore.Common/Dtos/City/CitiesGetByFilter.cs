using BookStore.Common.Dtos.Province;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.City;

public record CitiesGetByFilter(string? Name, int? Skip, int? Take, CityOrderBy? Order);