using FilmsCatalog.BLL.Interfaces;
using FilmsCatalog.Models.Film;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using FilmsCatalog.Helpers.Image;
using Microsoft.AspNetCore.Hosting;
using FilmsCatalog.BLL.DTO.Film;

namespace FilmsCatalog.Controllers
{
    
    public class FilmController : Controller
    {
        private readonly IFilmService _filmService;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public FilmController(IFilmService filmService,
            IWebHostEnvironment webHostEnvironment)
        {
            _filmService = filmService;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        [ValidateAntiForgeryToken]
        public IActionResult Index()
        {
            return View();
        }
        
        public ActionResult GetFilms(string message)
        {
            var films = _filmService.GetFilms();

            var result = new FilmCatalogVM();
            result.Films = new List<FilmVM>();

            films.ForEach(item =>
            {
                result.Films.Add(new FilmVM
                {
                    Id = item.Id,
                    Name = $"{item.Name} ({item.ReleaseYear.ToString()} г.)",
                    ImageName = item.ImgName
                    
                });
            });

            if(!string.IsNullOrEmpty(message))
                result.Meassage = message;

            return View("FilmCatalog", result);
        }

        [HttpGet]
        public ActionResult CreateFilm()
        {
            return View("CreateFilm", new CreateFilmVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFilm(CreateFilmVM createFilm)
        {
            if (!ModelState.IsValid)
            {
                return View(createFilm);
            }

            if (createFilm.ReleaseYear < 1895 || createFilm.ReleaseYear > DateTime.Now.Year)
            {
                ModelState.AddModelError(string.Empty, "Указан неверный год выпуска");
                return View(createFilm);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId == null)
            {
                ModelState.AddModelError(string.Empty, "Пользователь не авторизован");
                return View(createFilm);
            }

            var imageName = ImageHelper.UploadImage(createFilm.ImageFile, _webHostEnvironment);

            var filmCreateDto = new FilmForCreateDto
            {
                Name = createFilm.Name,
                Description = createFilm.Description,
                ReleaseYear = createFilm.ReleaseYear,
                Director = createFilm.Director,
                ImgName = imageName,
                UserId = userId
            };

            _filmService.CreateFilm(filmCreateDto);

            var alertMessage = $"Фильм {createFilm.Name} успешно создан!";

            return RedirectToAction("GetFilms", "Film", new { message = alertMessage });
        }

        
    }
}
