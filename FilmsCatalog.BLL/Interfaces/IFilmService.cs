using FilmsCatalog.BLL.DTO.Film;
using FilmsCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FilmsCatalog.BLL.Interfaces
{
    public interface IFilmService
    {
        int CreateFilm(FilmForCreateDto filmDto);
        FilmForReturnDto GetFilm(int? id);
        List<FilmForReturnDto> GetFilms();
        IQueryable<FilmEntity> GetAllQuery();
        int UpdateFilm(FilmForUpdateDto filmForUpdateDto);
    }
}
