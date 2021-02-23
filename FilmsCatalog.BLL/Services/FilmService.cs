using FilmsCatalog.BLL.DTO.Film;
using FilmsCatalog.BLL.Infrastructure;
using FilmsCatalog.BLL.Interfaces;
using FilmsCatalog.Domain.Entities;
using FilmsCatalog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmsCatalog.BLL.Services
{
    public class FilmService : IFilmService
    {
        private readonly IRepositoryManager _repositoryManager;

        public FilmService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public void CreateFilm(FilmForCreateDto filmDto)
        {
            var film = new FilmEntity
            {
                Name = filmDto.Name,
                Description = filmDto.Description,
                ReleaseYear = filmDto.ReleaseYear,
                Director = filmDto.Director,
                ImgName = filmDto.ImgName,
                CreatedDateTime = DateTime.Now,
                CreatedBy = filmDto.UserId                
            };

            _repositoryManager.Films.Create(film);
            _repositoryManager.SaveChanges();
        }

        public FilmDto GetFilm(int? id)
        {
            if (id == null)
                throw new ValidationException("Не указан id фильма", "");

            var film = _repositoryManager.Films.Get(id.Value);

            if(film == null)
                throw new ValidationException("Фильм не найден", "");

            return new FilmDto
            {
                Name = film.Name,
                Description = film.Description,
                ReleaseYear = film.ReleaseYear,
                Director = film.Director,
                ImgName = film.ImgName
            };
        }

        public List<FilmForReturnDto> GetFilms()
        {
            var films = _repositoryManager.Films.GetAll();

            var result = new List<FilmForReturnDto>();

            foreach(var item in films)
            {
                result.Add(new FilmForReturnDto(item));
            }

            return result;
        }
    }
}
