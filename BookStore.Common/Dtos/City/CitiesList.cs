using BookStore.Common.Dtos.Province;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.City;

public record CitiesList(string Id, string Name, ProvincesList Province);
