using System.ComponentModel.DataAnnotations;

namespace WebApiEntityFrameworkCoreDemo.DTOs
{
    public class BookRequest
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; }
        [Required]
        [StringLength(500)]
        public string Description { get; set; }
        [Required]
        public decimal Price { get; set; }
        public List<Guid> AuthorIds { get; set; } = new();
    }
}
