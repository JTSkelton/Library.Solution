using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
  public class Book
  {
    public Book()
        {
            this.BookPatronEntities = new HashSet<BookPatron>();
            this.BookLibrarianEntities = new HashSet<BookLibrarian>();
        }

    [Key]
    public int BookId { get; set; }
    public int Copies { get; set; }
    public string Title { get; set; }
    public string Author{ get; set; }
    public DateTime DateDue{ get; set; } = DateTime.Now.AddDays(14);
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<BookPatron> BookPatronEntities { get;}
    public virtual ICollection<BookLibrarian> BookLibrarianEntities { get;}
    
  }
}

// DateTime dt4 = dt.AddDays(65);
// DateTime today = DateTime.Now;
//         DateTime answer = today.AddDays(36);