using FilmsCatalog.BLL.Interfaces;
using FilmsCatalog.Models.Film;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalog.Controllers
{
    public class FilmController : Controller
    {
        private readonly IFilmService _filmService;

        public FilmController(IFilmService filmService)
        {
            _filmService = filmService;
        }

        public IActionResult Index()
        {
            return View();
        }
        
        public ActionResult GetFilms()
        {
            var films = _filmService.GetFilms();

            var result = new FilmCatalogVM();
            result.Films = new List<FilmVM>();

            films.ForEach(item =>
            {
                result.Films.Add(new FilmVM
                {
                    Id = item.Id,
                    Name = $"{item.Name} ({item.ReleaseYear.ToString()} г.)"
                });
            });

            return View("FilmCatalog", result);
        }
    }
}
