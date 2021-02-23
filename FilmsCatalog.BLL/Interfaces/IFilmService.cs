using FilmsCatalog.BLL.DTO.Film;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmsCatalog.BLL.Interfaces
{
    public interface IFilmService
    {
        void CreateFilm(FilmForCreateDto filmDto);
        FilmDto GetFilm(int? id);
        List<FilmForReturnDto> GetFilms();
    }
}
