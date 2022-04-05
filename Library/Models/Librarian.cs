using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
  public class Librarian
  {
    [Key]
    public int LibrarianId { get; set; }

    [Required]
    public string LibrarianName { get; set; }
  }
}
