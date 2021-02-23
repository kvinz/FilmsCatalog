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

        public int CreateFilm(FilmForCreateDto filmDto)
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
            return _repositoryManager.SaveChanges();
        }

        public FilmForReturnDto GetFilm(int? id)
        {
            if (id == null)
                throw new ValidationException("Не указан id фильма", "");

            var film = _repositoryManager.Films.Get(id.Value);

            if(film == null)
                throw new ValidationException("Фильм не найден", "");

            return new FilmForReturnDto(film);
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

        public int UpdateFilm(FilmForUpdateDto filmForUpdateDto)
        {
            var film = _repositoryManager.Films.Get(filmForUpdateDto.Id);

            if (film == null)
                throw new ValidationException("Фильм не найден", "");

            filmForUpdateDto.UpdateEntity(film);
            
            _repositoryManager.Films.Update(film);
           return  _repositoryManager.SaveChanges();
        }
    }
}
