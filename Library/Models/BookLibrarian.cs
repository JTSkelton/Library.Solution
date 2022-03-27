using System.ComponentModel.DataAnnotations;
namespace Library.Models
{
  public class BookLibrarian
  {       

     [Key] 
    public int BookLibrarianId { get; set; }
    public int BookId { get; set; }
    public int LibrarianId { get; set; }
    public virtual Book Book { get; set; }
    public virtual Librarian Librarian { get; set; }
  }
}