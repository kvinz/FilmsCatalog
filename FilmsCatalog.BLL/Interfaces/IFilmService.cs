using FilmsCatalog.BLL.DTO.Film;
using FilmsCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsCatalog.BLL.Interfaces
{
    public interface IFilmService
    {
        Task<int> CreateFilmAsync(FilmForCreateDto filmDto);
        Task<FilmForReturnDto> GetFilmAsync(int? id);
        Task<List<FilmForReturnDto>> GetFilmsAsync();
        IQueryable<FilmEntity> GetAllQuery();
        Task<int> UpdateFilmAsync(FilmForUpdateDto filmForUpdateDto);
    }
}
