using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Library.Models
{
  public class Patron
  {
    public Patron()
    {
      this.JoinEntities = new HashSet<BookPatron>();
    }
    [Key]
    public int PatronId { get; set; }
    public string PatronName { get; set; }
    public virtual ICollection<BookPatron> JoinEntities { get; set; }
  }
}