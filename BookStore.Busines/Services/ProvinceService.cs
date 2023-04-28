using AutoMapper;
using BookStore.Busines.Configuration;
using BookStore.Common.Dtos.Author;
using BookStore.Common.Dtos.Book;
using BookStore.Common.Dtos.City;
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

public class ProvinceService : IProvinceService
{
    private readonly IUnitOfWork _unitOfWork;
    private readonly IMapper _mapper;
    public ProvinceService(IUnitOfWork unitOfWork, IMapper mapper)
    {
        _unitOfWork = unitOfWork;
        _mapper = mapper;
    }

    public async Task<string?> CreatProvinceAsync(ProvinceCreate provinceCreate)
    {
        var mappedProvince = _mapper.Map<Province>(provinceCreate);

        if (provinceCreate.CitiesName != null)
        {
            mappedProvince.Cities = provinceCreate.CitiesName.Select(p => new City()
            {
                Id = Guid.NewGuid().ToString(),
                Name = p
            }).ToList();
        }

        var provinceId = await _unitOfWork.Province.Insert(mappedProvince);

        await _unitOfWork.CompleteAsync();
        await _unitOfWork.Dispose();

        return provinceId.ToString();
    }

    public async Task DeleteProvinceAsync(ProvinceDelete provinceDelete)
    {
        Province? province = await _unitOfWork.Province.GetByIdAsync(provinceDelete.Id);
        if (province != null)
        {
            _unitOfWork.Province.Delete(province);

            await _unitOfWork.CompleteAsync();
            await _unitOfWork.Dispose();
        }
    }

    public async Task UpdateProvinceAsync(ProvinceUpdate provinceUpdate)
    {
        var mappedProvince = _mapper.Map<Province>(provinceUpdate);
        _unitOfWork.Province.Update(mappedProvince);

        await _unitOfWork.CompleteAsync();
        await _unitOfWork.Dispose();
    }


    public async Task<ProvinceGet?> GetProvinceByIdAsync(string id)
    {
        var province = await _unitOfWork.Province.GetByIdAsync(id, p => p.Cities);

        if (province != null)
        {
            var provinceMapped = _mapper.Map<ProvinceGet>(province);
            provinceMapped.Cities = _mapper.Map<List<CitiesListWithoutProvince>>(province.Cities);

            return provinceMapped;
        }
        return null;
    }


    public async Task<IEnumerable<ProvincesList>> GetProvincesAsync()
    {
        var province = await _unitOfWork.Province.GetAsync(null, null, null);
        var provincesMapped = _mapper.Map<List<ProvincesList>>(province);
        return provincesMapped;
    }


    public async Task<IEnumerable<ProvincesList>> GetProvincesByFilter(ProvincesGetByFilter provincesGetByFilter)
    {
        Func<IQueryable<Province>, IOrderedQueryable<Province>>? userOrderType = null;
        if (provincesGetByFilter.Order == ProvinceOrderBy.Id)
        {
            userOrderType = (a) => a.OrderBy(y => y.Id);
        }
        else if (provincesGetByFilter.Order == ProvinceOrderBy.Name)
        {
            userOrderType = (a) => a.OrderBy(y => y.Name);
        }


        string name = provincesGetByFilter.Name;
        Expression<Func<Province, bool>> filterName = (p) => !name.IsNullOrEmpty() ?
             p.Name.ToLower().Contains(name.Trim().ToLower()) : true;


        var provinces = await _unitOfWork.Province
            .GetFilteredAsync(
            new Expression<Func<Province, bool>>[]
            {
                filterName
            },
            userOrderType,
            provincesGetByFilter.Skip,
            provincesGetByFilter.Take);

        if (provinces != null)
        {
            var provincesMapped = _mapper.Map<List<ProvincesList>>(provinces);
            return provincesMapped;
        }
        return null;
    }
}
