using System.ComponentModel.DataAnnotations;
namespace Library.Models
{
  public class BookPatron
  {       
    [Key]
    public int BookPatronId { get; set; }
    public int BookId { get; set; }
    public int PatronId { get; set; }
    public System.DateTime DateDue { get; set; } = System.DateTime.Now.AddDays(14);
    public virtual Book Book { get; set; }
    public virtual Patron Patron { get; set; }
  }
}