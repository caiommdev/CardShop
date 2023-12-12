using CardShop.Models;
using Microsoft.EntityFrameworkCore;

namespace CardShop.Data
{
    public class CardShopContext : DbContext
    {
        public CardShopContext (DbContextOptions<CardShopContext> options)
            : base(options)
        {
        }

        public DbSet<Card> Card { get; set; } = default!;
    }
}
