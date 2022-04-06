using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;
using System;
namespace Library.Controllers
{

  [Authorize]
  public class PatronsController : Controller
  {
    private readonly LibraryContext _db;
    //private readonly UserManager<ApplicationUser> _userManager;

    public PatronsController(LibraryContext db)
    {
    // public PatronsController(UserManager<ApplicationUser> userManager, LibraryContext db)
    // {
    //   _userManager = userManager;
      _db = db;
    }
    
    [AllowAnonymous]
    public ActionResult Index()
    {
      List<Patron> model = _db.Patrons.ToList();
      return View(model);
    }


    [AllowAnonymous]
    public ActionResult Create()
    {
      return View();
    }

    [AllowAnonymous]
    [HttpPost]
    public ActionResult Create(Patron author)
    {
      _db.Patrons.Add(author);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

   [AllowAnonymous]
    public ActionResult Details(int id)
    {
      var thisPatron = _db.Patrons
          .Include(author => author.BookPatronEntities)
          .ThenInclude(join => join.Book)
          .FirstOrDefault(author => author.PatronId == id);
      return View(thisPatron);
    }
    public ActionResult Edit(int id)
    {
      var thisPatron = _db.Patrons.FirstOrDefault(author => author.PatronId == id);
      return View(thisPatron);
    }

    [HttpPost]
    public ActionResult Edit(Patron author)
    {
      _db.Entry(author).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisPatron = _db.Patrons.FirstOrDefault(author => author.PatronId == id);
      return View(thisPatron);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisPatron = _db.Patrons.FirstOrDefault(author => author.PatronId == id);
      _db.Patrons.Remove(thisPatron);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
    // public ActionResult CheckoutBook (Book book, int PatronId, int id, int Copies, int CheckedOutCopies)
    // {
    //   if (PatronId != 0)
    //   {
    //     _db.BookPatron.Add(new BookPatron() { PatronId = PatronId, BookId = book.BookId });
    //     _db.SaveChanges();
    //   }
    //   book.Copies = Copies - 1;
    //   book.CheckedOutCopies = CheckedOutCopies + 1;
    //   _db.Entry(book).State = EntityState.Modified;
    //   _db.SaveChanges();
    //   return RedirectToAction("Index");
    // }


  }
}