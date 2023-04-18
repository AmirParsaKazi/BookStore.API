using AutoMapper;
using BookStore.Busines.Configuration;
using BookStore.Common.Dtos.Author;
using BookStore.Common.Dtos.Book;
using BookStore.Common.Dtos.Language;
using BookStore.Common.Models;
using BookStore.Common.Models.Interfaces.IServices;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Busines.Services
{
    public class LanguageService : ILanguageService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;
        public LanguageService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<string?> CreatLanguageAsync(LanguageCreate languageCreate)
        {
            var mappedLanguage = _mapper.Map<Language>(languageCreate);
            var languageId = await _unitOfWork.Language.Insert(mappedLanguage);
            
            await _unitOfWork.CompleteAsync();
            await _unitOfWork.Dispose();

            return languageId.ToString();
        }

        public async Task DeleteLanguageAsync(LanguageDelete languageDelete)
        {
            Language? language = await _unitOfWork.Language.GetByIdAsync(languageDelete.Id);
            if (language != null)
            {
                _unitOfWork.Language.Delete(language);

                await _unitOfWork.CompleteAsync();
                await _unitOfWork.Dispose();
            }
        }

        public async Task<IEnumerable<LanguageList>> GetLanguagesAsync()
        {
            var languages = await _unitOfWork.Language.GetAsync(null, null, null);
            var languagesMapped = _mapper.Map<List<LanguageList>>(languages);
            return languagesMapped;
        }

        public async Task<IEnumerable<LanguageList>> GetLanguageByFilter(LanguageGetByFilter languageGetByFilter)
        {
            Func<IQueryable<Language>, IOrderedQueryable<Language>>? userOrderType = null;
            if (languageGetByFilter.Order == LanguageOrderBy.Id)
            {
                userOrderType = (a) => a.OrderBy(y => y.Id);
            }
            else if (languageGetByFilter.Order == LanguageOrderBy.Name)
            {
                userOrderType = (a) => a.OrderBy(y => y.Name);
            }

            string name = languageGetByFilter.Name;
            Expression<Func<Language, bool>> filterName = (p) => !name.IsNullOrEmpty() ?
                 p.Name.ToLower().Contains(name.Trim().ToLower()) : true;

            var languages = await _unitOfWork.Language
                .GetFilteredAsync(
                new Expression<Func<Language, bool>>[]
                {
                    filterName
                },
                userOrderType,
                languageGetByFilter.Skip,
                languageGetByFilter.Take);

            if (languages != null)
            {
                var languageMapped = _mapper.Map<List<LanguageList>>(languages);
                return languageMapped;
            }
            return null;
        }

        public async Task<LanguageGet?> GetLanguageByIdAsync(string id)
        {
            var language = await _unitOfWork.Language.GetByIdAsync(id, p => p.Books);

            if (language != null)
            {
                var languageMapped = _mapper.Map<LanguageGet>(language);
                languageMapped.Books = _mapper.Map<List<BooksList>>(language.Books) ;
                return languageMapped;
            }
            return null;
        }

        public async Task UpdateLanguageAsync(LanguageUpdate languageUpdate)
        {
            var mappedLanguage = _mapper.Map<Language>(languageUpdate);
            _unitOfWork.Language.Update(mappedLanguage);

            await _unitOfWork.CompleteAsync();
            await _unitOfWork.Dispose();
        }
    }
}
