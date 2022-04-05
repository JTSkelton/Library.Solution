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
        }

    [Key]
    public int BookId { get; set; }
    [Required(ErrorMessage = "Please enter copies")]
    public int Copies { get; set; }
    public int CheckedOutCopies { get; set; }
    [Required(ErrorMessage = "Please enter title")] 
    public string Title { get; set; }
    public string Author{ get; set; }
    public string Genre{ get; set; }
    public bool IsCheckedOut { get; set; }
    public virtual ApplicationUser User { get; set; }
    public virtual ICollection<BookPatron> BookPatronEntities { get;}
    
  }
}
