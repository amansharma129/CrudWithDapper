using System.ComponentModel.DataAnnotations;

namespace crud.Models
{
    public class Product
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public string Name { get; set; }
        public string Description { get; set; }
        public DateTime CreatedAt { get; set; }

        [Required]
        public decimal Price { get; set; }
    }
}
