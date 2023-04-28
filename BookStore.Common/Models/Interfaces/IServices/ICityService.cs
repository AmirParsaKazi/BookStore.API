using BookStore.Common.Dtos.City;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Models.Interfaces.IServices; 
public interface ICityService 
{
    Task<string?> CreatCityAsync(CityCreate cityCreate);
    Task UpdateCityAsync(CityUpdate cityUpdate);
    Task DeleteCityAsync(CityDelete cityDelete);
    Task<CityGet?> GetCityByIdAsync(string id);
    Task<CityGetWithoutCustomerInfo?> GetCityByIdWithoutCustomersInfoAsync(string id);
    Task<IEnumerable<CitiesList>> GetCitiesAsync();
    Task<IEnumerable<CitiesList>> GetCitiesByFilter(CitiesGetByFilter citiesGetByFilter);
}
