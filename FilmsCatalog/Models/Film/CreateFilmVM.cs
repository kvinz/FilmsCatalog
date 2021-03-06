﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalog.Models.Film
{
    public class CreateFilmVM
    {
        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Название*")]
        [StringLength(255)]
        public string Name { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Описание*")]
        [StringLength(2000)]
        public string Description { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Год выпуска*")]
        public int ReleaseYear { get; set; }

        [Required(ErrorMessage = "Обязательное поле")]
        [Display(Name = "Режиссёр*")]
        [StringLength(255)]
        public string Director { get; set; }

        [Required(ErrorMessage = "Пожалуйста загрузите постер")]
        [Display(Name = "Постер*")]
        public IFormFile ImageFile { get; set; }
    }
}
