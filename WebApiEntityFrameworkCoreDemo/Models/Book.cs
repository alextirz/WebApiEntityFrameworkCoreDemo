using System.ComponentModel.DataAnnotations;

namespace WebApiEntityFrameworkCoreDemo.Models
{
    public class Book
    {
        [Key]
        public Guid Id { get; set; }
        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public virtual IEnumerable<Author> Authors { get; set; }
    }
}
