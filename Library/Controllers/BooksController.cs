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
  public class BooksController : Controller
  {
    private readonly LibraryContext _db;
    private readonly UserManager<ApplicationUser> _userManager; 

    public BooksController(UserManager<ApplicationUser> userManager, LibraryContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    // public ActionResult Index(string searchBy, string search)
    // {
    //   List<Book> model = _db.Books.ToList();
    //   return View(model);
    //   if (searchBy == "Title")
    //   {
    //     return View(_db.Books.Where(x => x.Title.StartsWith(search) || search == null).ToList());
    //   }
    //   else
    //   {
    //     return View(_db.Authors.Where(x => x.AuthorName.StartsWith(search) || search == null).ToList());
    //   }
    //   // else
    //   // {
    //   // List<Book> model = _db.Books.ToList();
    //   // return View(model);
    //   // }

    // }

//     public async Task<ActionResult> Index()
// {
//     var userId = this.User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//     var currentUser = await _userManager.FindByIdAsync(userId);
//     var userItems = _db.Items.Where(entry => entry.User.Id == currentUser.Id).ToList();
//     return View(userItems);
//last line shows only what the users puts in in the db items 

//for us the first two lines are the same (setting up the user id) then shows only what the user touches on the last line.  maybe another view that has books viewed by the user
//we use var userBooks = _db.Books.Where etc to have the user only see the books they have checked out.. 
// }

    [AllowAnonymous]
    public async Task<IActionResult> Index(string searchString)
    {
      var books = from m in _db.Books
      select m;

    if (!String.IsNullOrEmpty(searchString))
    {
        books = books.Where(s => s.Title!.Contains(searchString));
    }
      return View(await books.ToListAsync());
    }
  [Authorize]
    public ActionResult Create()
    {
      ViewBag.LibrarianId = new SelectList(_db.Librarians, "LibrarianId", "LibrarianName");
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "AuthorName");
      return View();
    }
  [Authorize]
    [HttpPost]
    public ActionResult Create(Book book, int LibrarianId, int AuthorId)
    {
        _db.Books.Add(book);
        _db.SaveChanges();
        if (LibrarianId != 0)
        {
            _db.AuthorBookLibrarian.Add(new AuthorBookLibrarian() { LibrarianId = LibrarianId, BookId = book.BookId, AuthorId = AuthorId });
        }
        _db.SaveChanges();
        return RedirectToAction("Index");
    }
  [Authorize]
    public ActionResult Details(int id)
    {
      var thisBook = _db.Books
          .Include(book => book.AuthorBookLibrarianEntities)
          .ThenInclude(join => join.Librarian)
          .Include(book => book.BookPatronEntities)
          .ThenInclude(join => join.Patron)
          .FirstOrDefault(book => book.BookId == id);
      return View(thisBook);
    }
  [Authorize]
    public ActionResult Edit(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
      ViewBag.LibrarianId = new SelectList(_db.Librarians, "LibrarianId", "LibrarianName");
      return View(thisBook);
    }
  [Authorize]
    [HttpPost]
    public ActionResult Edit(Book book, int LibrarianId, int AuthorId)
    {
      if (LibrarianId != 0)
      {
        _db.AuthorBookLibrarian.Add(new AuthorBookLibrarian() { LibrarianId = LibrarianId, BookId = book.BookId, AuthorId = AuthorId });
      }
      _db.Entry(book).State = EntityState.Modified;
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    // public ActionResult AddLibrarian(int id)
    // {
    //   var thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
    //   ViewBag.LibrarianId = new SelectList(_db.Librarians, "LibrarianId", "LibrarianName");
    //   return View(thisBook);
    // }

    // [HttpPost]
    // public ActionResult AddLibrarian(Book book, int LibrarianId)
    // {
    //   if (LibrarianId != 0)
    //   {
    //     _db.BookLibrarian.Add(new BookLibrarian() { LibrarianId = LibrarianId, BookId = book.BookId });
    //     _db.SaveChanges();
    //   }
    //   return RedirectToAction("Index");
    // }
  [Authorize]
    public ActionResult AddPatron(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
      ViewBag.PatronId = new SelectList(_db.Patrons, "PatronId", "PatronName");
      return View(thisBook);
    }
  [Authorize]
    [HttpPost]
    public ActionResult AddPatron(Book book, int PatronId)
    {
      if (PatronId != 0)
      {
        _db.BookPatron.Add(new BookPatron() { PatronId = PatronId, BookId = book.BookId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }
  [Authorize]
    public ActionResult AddAuthor(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
      ViewBag.AuthorId = new SelectList(_db.Authors, "AuthorId", "AuthorName");
      return View(thisBook);
    }
  [Authorize]
    [HttpPost]
    public ActionResult AddAuthor(Book book, int AuthorId)
    {
      if (AuthorId != 0)
      {
        _db.AuthorBookLibrarian.Add(new AuthorBookLibrarian() { AuthorId = AuthorId, BookId = book.BookId });
        _db.SaveChanges();
      }
      return RedirectToAction("Index");
    }
  [Authorize]
    public ActionResult Delete(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
      return View(thisBook);
    }
  [Authorize]
    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      var thisBook = _db.Books.FirstOrDefault(book => book.BookId == id);
      _db.Books.Remove(thisBook);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  [Authorize]
    [HttpPost]
    public ActionResult DeletePatron(int joinId)
    {
      var joinEntry = _db.BookPatron.FirstOrDefault(entry => entry.BookPatronId == joinId);
      _db.BookPatron.Remove(joinEntry);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}
