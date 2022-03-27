using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
  public class Librarian
  {
    public Librarian()
    {
      this.AuthorBookLibrarianEntities = new HashSet<AuthorBookLibrarian>();
    }
    [Key]
    public int LibrarianId { get; set; }
    public string LibrarianName { get; set; }
    public virtual ICollection<AuthorBookLibrarian> AuthorBookLibrarianEntities { get; set; }
  }
}