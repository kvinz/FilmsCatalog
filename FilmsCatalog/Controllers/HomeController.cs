using FilmsCatalog.BLL.DTO.Film;
using FilmsCatalog.BLL.Interfaces;
using FilmsCatalog.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalog.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IFilmService _filmService;

        public HomeController(ILogger<HomeController> logger,
            IFilmService filmService)
        {
            _logger = logger;
            _filmService = filmService;
        }

        public IActionResult Index()
        {
            //try
            //{
            //    var filmDto = new FilmDto
            //    {
            //        Name = "Крестный отец",
            //        Description = "Фильм про мафию",
            //        Director = "Ф. Ф. Копола",
            //        ImgName = "хз",
            //        ReleaseYear = 1111
            //    };

            //    _filmService.CreateFilm(filmDto);
            //}
            //catch(Exception ex)
            //{

            //}

            var film = _filmService.GetFilm(1);

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
