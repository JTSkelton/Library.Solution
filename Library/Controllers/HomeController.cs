using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Library.Models;
using Microsoft.AspNetCore.Authorization;

namespace Library.Controllers
{
    public class HomeController : Controller
    {
      [HttpGet("/")]
      public ActionResult Index()
      {
        return View();
      }

      [Authorize(Roles = "Admin")]
      public IActionResult Admin() => View();

      [Authorize(Roles = "Librarian")]
      public IActionResult Librarian() => View();

      [Authorize(Roles = "Patron")]
      public IActionResult Patron() => View();


      public IActionResult Error() => View(new Models.ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}