using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Dtos.Province;

public record ProvincesGetByFilter(string? Name, int? Skip, int? Take, ProvinceOrderBy? Order);
