using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class HomeController : Controller
    {

      [HttpGet("/")]
      public ActionResult LoginIndex()
      {
        return View();
      }

    }
}