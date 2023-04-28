using AutoMapper;
using BookStore.Busines.Configuration;
using BookStore.Common.Dtos.Book;
using BookStore.Common.Dtos.City;
using BookStore.Common.Dtos.Customer;
using BookStore.Common.Dtos.Province;
using BookStore.Common.Models;
using BookStore.Common.Models.Interfaces.IServices;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Busines.Services;

public class CityService : ICityService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public CityService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }


    public async Task<string?> CreatCityAsync(CityCreate cityCreate)
    {
        var mappedCity = _mapper.Map<City>(cityCreate);

        var cityId = await _unitOfWork.City.Insert(mappedCity);

        await _unitOfWork.CompleteAsync();
        await _unitOfWork.Dispose();

        return cityId.ToString();
    }

    public async Task DeleteCityAsync(CityDelete cityDelete)
    {
        City? city = await _unitOfWork.City.GetByIdAsync(cityDelete.Id);
        if (city != null)
        {
            _unitOfWork.City.Delete(city);

            await _unitOfWork.CompleteAsync();
            await _unitOfWork.Dispose();
        }
    }

    public async Task UpdateCityAsync(CityUpdate cityUpdate)
    {
        var mappedCity = _mapper.Map<City>(cityUpdate);

        var isExist = await _unitOfWork.Province
            .GetByIdAsync(cityUpdate.ProvinceId);

        if (isExist != null)
        {
            _unitOfWork.City.Update(mappedCity);

            await _unitOfWork.CompleteAsync();
            await _unitOfWork.Dispose();
        }
    }

    public async Task<IEnumerable<CitiesList>> GetCitiesAsync()
    {
        var cities = await _unitOfWork.City.GetAsync(null, null, null, p => p.Province);
        var citiesMapped = _mapper.Map<List<CitiesList>>(cities);

        return citiesMapped;
    }

    public async Task<IEnumerable<CitiesList>> GetCitiesByFilter(CitiesGetByFilter citiesGetByFilter)
    {
        Func<IQueryable<City>, IOrderedQueryable<City>>? userOrderType = null;
        if (citiesGetByFilter.Order == CityOrderBy.Id)
        {
            userOrderType = (a) => a.OrderBy(y => y.Id);
        }
        else if (citiesGetByFilter.Order == CityOrderBy.Name)
        {
            userOrderType = (a) => a.OrderBy(y => y.Name);
        }

       

        Func<IEnumerable<CitiesList>, IOrderedEnumerable<CitiesList>>? orderByProvince = null;
        if (citiesGetByFilter.Order == CityOrderBy.Province)
        {
            orderByProvince = (a) => a.OrderBy(y => y.Province);
        }



        string name = citiesGetByFilter.Name;
        Expression<Func<City, bool>> filterName = (p) => !name.IsNullOrEmpty() ?
             p.Name.ToLower().Contains(name.Trim().ToLower()) : true;


        var cities = await _unitOfWork.City
            .GetFilteredAsync(
            new Expression<Func<City, bool>>[]
            {
                filterName
            },
            userOrderType,
            citiesGetByFilter.Skip,
            citiesGetByFilter.Take,
            p => p.Province);

        if (cities != null)
        {
            var citiesMapped = _mapper.Map<List<CitiesList>>(cities);
            if (orderByProvince != null)
            {
                orderByProvince(citiesMapped);
            }

            return citiesMapped;
        }
        return null;
    }

    public async Task<CityGet?> GetCityByIdAsync(string id)
    {
        var city = await _unitOfWork.City.GetByIdAsync(id, p => p.Province, q => q.Customers1, c => c.Customers2);

        if (city != null)
        {
            var cityMapped = _mapper.Map<CityGet>(city);

            return cityMapped;
        }
        return null;
    }


    public async Task<CityGetWithoutCustomerInfo?> GetCityByIdWithoutCustomersInfoAsync(string id)
    {
        var city = await _unitOfWork.City.GetByIdAsync(id, p => p.Province);

        if (city != null)
        {
            var cityMapped = _mapper.Map<CityGetWithoutCustomerInfo>(city);

            return cityMapped;
        }
        return null;
    }
}
