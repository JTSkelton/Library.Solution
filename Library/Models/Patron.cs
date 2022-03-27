using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
  public class Patron
  {
    public Patron()
    {
      this.BookPatronEntities = new HashSet<BookPatron>();
    }
    [Key]
    public int PatronId { get; set; }
    public string PatronName { get; set; }
    public virtual ICollection<BookPatron> BookPatronEntities { get; set; }
  }
}