using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.City;

public record CityUpdate(string Id, string Name, string ProvinceId);
