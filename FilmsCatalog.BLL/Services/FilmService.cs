using FilmsCatalog.BLL.DTO.Film;
using FilmsCatalog.BLL.Infrastructure;
using FilmsCatalog.BLL.Interfaces;
using FilmsCatalog.Domain.Entities;
using FilmsCatalog.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FilmsCatalog.BLL.Services
{
    public class FilmService : IFilmService
    {
        private readonly IRepositoryManager _repositoryManager;

        public FilmService(IRepositoryManager repositoryManager)
        {
            _repositoryManager = repositoryManager;
        }

        public async Task<int> CreateFilmAsync(FilmForCreateDto filmDto)
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

            await _repositoryManager.Films.CreateAsync(film);
            return await _repositoryManager.SaveChanges();
        }

        public async Task<FilmForReturnDto> GetFilmAsync(int? id)
        {
            if (id == null)
                throw new ValidationException("Не указан id фильма", "");

            var film = await _repositoryManager.Films.GetAsync(id.Value);

            if(film == null)
                throw new ValidationException("Фильм не найден", "");

            return new FilmForReturnDto(film);
        }

        public async Task<List<FilmForReturnDto>> GetFilmsAsync()
        {
            var films = await _repositoryManager.Films.GetAllAsync();

            var result = new List<FilmForReturnDto>();

            foreach(var item in films)
            {
                result.Add(new FilmForReturnDto(item));
            }

            return result;
        }

        public IQueryable<FilmEntity> GetAllQuery()
        {
            return _repositoryManager.Films.GetAllQueryable();
        }

        public async Task<int> UpdateFilmAsync(FilmForUpdateDto filmForUpdateDto)
        {
            var film = await _repositoryManager.Films.GetAsync(filmForUpdateDto.Id);

            if (film == null)
                throw new ValidationException("Фильм не найден", "");

            filmForUpdateDto.UpdateEntity(film);
            
            _repositoryManager.Films.Update(film);
           return await  _repositoryManager.SaveChanges();
        }
    }
}
