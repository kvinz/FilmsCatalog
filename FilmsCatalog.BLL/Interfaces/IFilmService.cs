using FilmsCatalog.BLL.DTO.Film;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmsCatalog.BLL.Interfaces
{
    public interface IFilmService
    {
        void CreateFilm(FilmDto filmDto);

        FilmDto GetFilm(int? id);
    }
}
