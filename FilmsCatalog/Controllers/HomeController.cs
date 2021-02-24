using FilmsCatalog.BLL.DTO.Film;
using FilmsCatalog.BLL.Interfaces;
using FilmsCatalog.Models;
using Microsoft.AspNetCore.Identity;
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
        private readonly UserManager<UserEntity> _userManager;
        private readonly IFilmService _filmService;

        public HomeController(ILogger<HomeController> logger,
            IFilmService filmService,
            UserManager<UserEntity> userManager)
        {
            _logger = logger;
            _filmService = filmService;
            _userManager = userManager;
        }

        public async  Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);

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
