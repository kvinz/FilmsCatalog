using ReflectionIT.Mvc.Paging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalog.Models.Film
{
    public class FilmCatalogVM
    {
        public PagingList<FilmVM> Films { get; set; }

        public string Message { get; set; }

        public string MessageType { get; set; }
    }
}
