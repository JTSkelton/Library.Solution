using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
  public class Librarian
  {
    public Librarian()
    {
      this.JoinEntities = new HashSet<BookLibrarian>();
    }
    [Key]
    public int LibrarianId { get; set; }
    public string LibrarianName { get; set; }
    public virtual ICollection<BookLibrarian> JoinEntities { get; set; }
  }
}