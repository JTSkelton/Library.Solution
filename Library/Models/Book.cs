using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System;

namespace Library.Models
{
  public class Book
  {
    public Book()
        {
            this.BookPatronEntities = new HashSet<BookPatron>();
            this.AuthorBookLibrarianEntities = new HashSet<AuthorBookLibrarian>();
        }

    [Key]
    public int BookId { get; set; }
    [Required(ErrorMessage = "Please enter copies")]
    public int Copies { get; set; }
    public int CheckedOutCopies { get; set; }
    [Required(ErrorMessage = "Please enter title")] 
    public string Title { get; set; }
    // public string Author{ get; set; }
    public System.Boolean IsCheckedOut { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<BookPatron> BookPatronEntities { get;}
    public virtual ICollection<AuthorBookLibrarian> AuthorBookLibrarianEntities { get;}
    
  }
}

// DateTime dt4 = dt.AddDays(65);
// DateTime today = DateTime.Now;
//         DateTime answer = today.AddDays(36);