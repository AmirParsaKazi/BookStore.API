using BookStore.Common.Dtos.Author;
using BookStore.Common.Dtos.Language;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BookStore.Common.Models.Interfaces.IServices;

public interface ILanguageService
{
    Task<string?> CreatLanguageAsync(LanguageCreate languageCreate);
    Task UpdateLanguageAsync(LanguageUpdate languageUpdate);
    Task DeleteLanguageAsync(LanguageDelete languageDelete);
    Task<LanguageGet?> GetLanguageByIdAsync(string id);
    Task<IEnumerable<LanguageList>> GetLanguagesAsync();
    Task<IEnumerable<LanguageList>> GetLanguageByFilter(LanguageGetByFilter languageGetByFilter);

}
