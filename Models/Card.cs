using System.ComponentModel.DataAnnotations;

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
    }
}
