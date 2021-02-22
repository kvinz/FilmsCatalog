using System;
using System.Collections.Generic;
using System.Text;

namespace FilmsCatalog.BLL.DTO.Film
{
    public class FilmDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int ReleaseYear { get; set; }
        public string Director { get; set; }
        public string ImgName { get; set; }
    }
}
