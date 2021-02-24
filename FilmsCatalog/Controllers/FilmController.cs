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
using FilmsCatalog.BLL.Infrastructure;
using FilmsCatalog.Enums;
using System.IO;
using FilmsCatalog.Models;
using ReflectionIT.Mvc.Paging;
using Microsoft.EntityFrameworkCore;
using FilmsCatalog.Helpers.Pagination;

namespace FilmsCatalog.Controllers
{
    
    public class FilmController : Controller
    {
        private readonly IFilmService _filmService;
        private readonly IWebHostEnvironment _webHostEnvironment;
        private readonly UserManager<UserEntity> _userManager;

        public FilmController(IFilmService filmService,
            IWebHostEnvironment webHostEnvironment,
            UserManager<UserEntity> userManager)
        {
            _filmService = filmService;
            _webHostEnvironment = webHostEnvironment;
            _userManager = userManager;
        }

        [HttpGet]
        [ActionName("Index")]
        public async Task<ActionResult> Index(string message, MessageTypeEnum messageType, int pageIndex = 1)
        {
            var filmsQuery = _filmService.GetAllQuery().Select(x => new FilmVM
            {
                Id = x.Id,
                Name = x.Name,
                ReleaseYear = x.ReleaseYear,
                ImgName = x.ImgName
            })
                .OrderBy(o => o.Name);

            var model = await PagingList.CreateAsync(filmsQuery, PaginationHelper.CountItemsPerPage, pageIndex);

            model.ForEach(item =>
            {
                item.Name = $"{item.Name} ({item.ReleaseYear} г.)";
            });

            var result = new FilmCatalogVM();
            result.Films = model;

            if (!string.IsNullOrEmpty(message))
            {
                result.Message = message;
                result.MessageType = EnumHelper.GetDescription(messageType);
            }

            return View("FilmCatalog", result);
        }


        [HttpGet]
        public async Task<ActionResult> GetFilm(int id)
        {
            try
            {
                var user = await _userManager.GetUserAsync(User);

                if (user != null)
                {
                    var userName = $"{user.FirstName} {user.MiddleName} {user.LastName}";
                    var filmDto = _filmService.GetFilm(id);

                    return View("ViewFilm", new ViewFilmVM(filmDto, userName));
                }
                else
                    return RedirectToAction("SignIn", "Account");
            }
            catch (ValidationException ex)
            {
                return RedirectToAction("Index", "Film", new { message = ex.Message, messageType = MessageTypeEnum.Danger });
            }
        }

        [HttpGet]
        public ActionResult CreateFilm()
        {
            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if (userId == null)
            {
                var alertMessage = "Пользователь не авторизован";
                return RedirectToAction("Index", "Film", new { message = alertMessage, messageType = MessageTypeEnum.Danger });
            }

            return View("CreateFilm", new CreateFilmVM());
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult CreateFilm(CreateFilmVM createFilmModel)
        {
            if (!ModelState.IsValid)
            {
                return View(createFilmModel);
            }

            if (createFilmModel.ReleaseYear < 1895 || createFilmModel.ReleaseYear > DateTime.Now.Year)
            {
                ModelState.AddModelError(string.Empty, "Указан неверный год выпуска");
                return View(createFilmModel);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

            if(userId == null)
            {
                ModelState.AddModelError(string.Empty, "Пользователь не авторизован");
                return View(createFilmModel);
            }

            var imageName = ImageHelper.UploadImage(createFilmModel.ImageFile, _webHostEnvironment);

            var filmCreateDto = new FilmForCreateDto
            {
                Name = createFilmModel.Name,
                Description = createFilmModel.Description,
                ReleaseYear = createFilmModel.ReleaseYear,
                Director = createFilmModel.Director,
                ImgName = imageName,
                UserId = userId
            };

            _filmService.CreateFilm(filmCreateDto);

            var alertMessage = $"Фильм \"{createFilmModel.Name}\" успешно создан!";

            return RedirectToAction("Index", "Film", new { message = alertMessage, messageType = MessageTypeEnum.Success});
        }

        [HttpGet]
        public ActionResult UpdateFilm(int id)
        {
            try
            {
                var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);

                if (userId == null)
                {
                    var alertMessage = "Пользователь не авторизован";
                    return RedirectToAction("Index", "Film", new { message = alertMessage, messageType = MessageTypeEnum.Danger });
                }

                var filmDto = _filmService.GetFilm(id);

                if(string.IsNullOrEmpty(filmDto.UserId) || !filmDto.UserId.Equals(userId))
                {
                    var alertMessage = "Нет доступа к редактированию фильма";
                    return RedirectToAction("Index", "Film", new { message = alertMessage, messageType = MessageTypeEnum.Danger });
                }

                return View("UpdateFilm", new UpdateFilmVM(filmDto));
            }
            catch (ValidationException ex)
            {
                return RedirectToAction("Index", "Film", new { message = ex.Message, messageType = MessageTypeEnum.Danger });
            }
        }

        [HttpPost]
        public ActionResult UpdateFilm(UpdateFilmVM updateFilmModel)
        {
            var imageName = "";
            var alertMessage = "";

            if (!ModelState.IsValid)
            {
                return View(updateFilmModel);
            }

            var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
            if (userId == null)
            {
                ModelState.AddModelError(string.Empty, "Пользователь не авторизован");
                return View(updateFilmModel);
            }                                 

            // Если обновляется постер для фильма
            if (updateFilmModel.ImageFile != null)
            {
                imageName = ImageHelper.UploadImage(updateFilmModel.ImageFile, _webHostEnvironment);

                // Удаляем старый файл
                var filmDto = _filmService.GetFilm(updateFilmModel.Id);
                string uploadsFolder = Path.Combine(_webHostEnvironment.WebRootPath, "images");
                string filePath = Path.Combine(uploadsFolder, filmDto.ImgName);
                if(!string.IsNullOrEmpty(filmDto.ImgName))
                    System.IO.File.Delete(filePath);
            }

            var filmForUpdateDto = new FilmForUpdateDto
            {
                Id = updateFilmModel.Id,
                Name = updateFilmModel.Name,
                Description = updateFilmModel.Description,
                ReleaseYear = updateFilmModel.ReleaseYear,
                Director = updateFilmModel.Director,
                ImgName = imageName,
                UserId = userId
            };

            try
            {
                var result = _filmService.UpdateFilm(filmForUpdateDto);

                if(result == 0)
                {
                     alertMessage = $"Обновить фильм не удалось !";
                    return RedirectToAction("Index", "Film", new { message = alertMessage, messageType = MessageTypeEnum.Success });
                }
            }
            catch (ValidationException ex)
            {
                return RedirectToAction("Index", "Film", new { message = ex.Message, messageType = MessageTypeEnum.Danger });
            }

            alertMessage = $"Фильм \"{updateFilmModel.Name}\" успешно обновлен!";
            return RedirectToAction("Index", "Film", new { message = alertMessage, messageType = MessageTypeEnum.Success });
        }

    }
}
