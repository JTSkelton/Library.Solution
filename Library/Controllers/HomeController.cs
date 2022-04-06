using Microsoft.AspNetCore.Mvc;

namespace Library.Controllers
{
    public class HomeController : Controller
    {

      [HttpGet("Books")]
      public ActionResult Index()
      {
        return View();
      }

    }
}