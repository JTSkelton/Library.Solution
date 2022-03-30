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
  public class LibrariansController : Controller
  {
    private readonly LibraryContext _db;

    public LibrariansController(LibraryContext db)
    {
      _db = db;
    }

    public async Task<IActionResult> Index(string searchString)
    {
      var librarians = from m in _db.Librarians
      select m;

    if (!String.IsNullOrEmpty(searchString))
    {
        librarians = librarians.Where(s => s.LibrarianName!.Contains(searchString));
    }
      return View(await librarians.ToListAsync());
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