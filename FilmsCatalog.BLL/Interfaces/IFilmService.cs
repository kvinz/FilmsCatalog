using FilmsCatalog.BLL.DTO.Film;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmsCatalog.BLL.Interfaces
{
    public interface IFilmService
    {
        int CreateFilm(FilmForCreateDto filmDto);
        FilmForReturnDto GetFilm(int? id);
        List<FilmForReturnDto> GetFilms();
        int UpdateFilm(FilmForUpdateDto filmForUpdateDto);
    }
}
