using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CardShop.Models
{
    public class Card
    {
        [Key]
        public int Id { get; set; }
        [Required]
        public int Price { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public CardGame Game { get; set; }
        public DateTime? CreationDate { get; set; }
        
        public string? ImageFile { get; set; }
        public string? ImageFileName { get; set; }

        [NotMapped]
        public IFormFile Upload { get; set; }
    }
}
