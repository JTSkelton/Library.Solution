using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Library.Models;
using System.Collections.Generic;
using System.Linq;

namespace Library.Controllers
{
  public class LibrariansController : Controller
  {
    private readonly LibraryContext _db;

    public LibrariansController(LibraryContext db)
    {
      _db = db;
    }

    public ActionResult Index()
    {
      List<Librarian> model = _db.Librarians.ToList();
      return View(model);
    }

    public ActionResult Create()
    {
      return View();
    }

    [HttpPost]
    public ActionResult Create(Librarian librarian)
    {
      _db.Librarians.Add(librarian);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Details(int id)
    {
      var thisLibrarian = _db.Librarians
          .Include(librarian => librarian.AuthorBookLibrarianEntities)
          .ThenInclude(join => join.Book)
          .FirstOrDefault(librarian => librarian.LibrarianId == id);
      return View(thisLibrarian);
    }
    public ActionResult Edit(int id)
    {
      var thisLibrarian = _db.Librarians.FirstOrDefault(librarian => librarian.LibrarianId == id);
      return View(thisLibrarian);
    }

    [HttpPost]
    public ActionResult Edit(Librarian librarian)
    {
      _db.Entry(librarian).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult Delete(int id)
    {
      var thisLibrarian = _db.Librarians.FirstOrDefault(librarian => librarian.LibrarianId == id);
      return View(thisLibrarian);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisLibrarian = _db.Librarians.FirstOrDefault(librarian => librarian.LibrarianId == id);
      _db.Librarians.Remove(thisLibrarian);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}