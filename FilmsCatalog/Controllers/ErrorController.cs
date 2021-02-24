using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FilmsCatalog.Controllers
{
    public class ErrorController : Controller
    {
        public ActionResult ShowError(string message)
        {
            return View("Error", message);
        }
    }
}
