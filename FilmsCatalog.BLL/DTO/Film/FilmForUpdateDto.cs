using FilmsCatalog.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FilmsCatalog.BLL.DTO.Film
{
    public class FilmForUpdateDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
        public string ImgName { get; set; }
        public string UserId { get; set; }

        public void UpdateEntity(FilmEntity entity)
        {
            entity.Name = Name;
            entity.Description = Description;
            entity.ReleaseYear = ReleaseYear;
            entity.Director = Director;
            entity.ImgName = string.IsNullOrEmpty(ImgName) ? entity.ImgName : ImgName;
        }
    }
}
