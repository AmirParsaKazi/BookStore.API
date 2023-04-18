using BookStore.Common.Dtos.Province;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Models.Interfaces.IServices;

public interface IProvinceService
{
    Task<string?> CreatProvinceAsync(ProvinceCreate provinceCreate);
    Task UpdateProvinceAsync(ProvinceUpdate provinceUpdate);
    Task DeleteProvinceAsync(ProvinceDelete provinceDelete);
    Task<ProvinceGet?> GetProvinceByIdAsync(string id);
    Task<IEnumerable<ProvincesList>> GetProvincesAsync();
    Task<IEnumerable<ProvincesList>> GetProvincesByFilter(ProvincesGetByFilter provincesGetByFilter);

}
