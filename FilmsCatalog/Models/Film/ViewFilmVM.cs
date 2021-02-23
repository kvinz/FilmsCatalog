using FilmsCatalog.BLL.DTO.Film;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalog.Models.Film
{
    public class ViewFilmVM
    {
        [Display(Name = "Название:")]
        public string Name { get; set; }

        [Display(Name = "Описание:")]
        public string Description { get; set; }

        [Display(Name = "Год выпуска:")]
        public int ReleaseYear { get; set; }

        [Display(Name = "Режиссёр:")]
        public string Director { get; set; }

        [Display(Name = "Добавил:")]
        public string UserName { get; set; }

        public string ImageName { get; set; }

        public ViewFilmVM(FilmForReturnDto filmForReturnDto, string userName)
        {
            Name = filmForReturnDto.Name;
            Description = filmForReturnDto.Description;
            ReleaseYear = filmForReturnDto.ReleaseYear;
            Director = filmForReturnDto.Director;
            ImageName = filmForReturnDto.ImgName;
            UserName = userName;
        }
    }
}
