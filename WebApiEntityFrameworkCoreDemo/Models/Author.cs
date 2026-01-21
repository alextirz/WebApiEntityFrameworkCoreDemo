using System.ComponentModel.DataAnnotations;

namespace WebApiEntityFrameworkCoreDemo.Models
{
    public class Author
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public virtual IEnumerable<Book> Books { get; set; }
    }
}
