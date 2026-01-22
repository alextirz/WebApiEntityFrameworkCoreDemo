using System.ComponentModel.DataAnnotations;

namespace WebApiEntityFrameworkCoreDemo.DTOs
{
    public class AuthorRequest
    {
        [Required]
        [StringLength(100)]
        public string Name { get; set; }
        public DateTime BirthDate { get; set; }
        public List<Guid> BookIds { get; set; } = new();
    }
}
