using FilmsCatalog.BLL.DTO.Film;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalog.Models.Film
{
    public class UpdateFilmVM
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Название")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Описание")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Год выпуска")]
        public int ReleaseYear { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Режиссёр")]
        public string Director { get; set; }

        [Display(Name = "Постер")]
        public IFormFile ImageFile { get; set; }

        public UpdateFilmVM() { }

        public UpdateFilmVM(FilmForReturnDto filmForReturnDto)
        {
            Id = filmForReturnDto.Id;
            Name = filmForReturnDto.Name;
            Description = filmForReturnDto.Description;
            ReleaseYear = filmForReturnDto.ReleaseYear;
            Director = filmForReturnDto.Director;
        }
    }
}
