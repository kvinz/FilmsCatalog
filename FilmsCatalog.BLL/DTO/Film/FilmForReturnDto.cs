using FilmsCatalog.Domain.Entities;
using FilmsCatalog.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmsCatalog.BLL.DTO.Film
{
    public class FilmForReturnDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
        public string ImgName { get; set; }
        public User User { get; set; }

        public FilmForReturnDto(FilmEntity entity)
        {
            if(entity == null)
            {
                return;
            }

            Id = entity.Id;
            Name = entity.Name;
            Description = entity.Description;
            ReleaseYear = entity.ReleaseYear;
            Director = entity.Director;
            ImgName = entity.ImgName;
        }
    }
}
