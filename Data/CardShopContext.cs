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

        public DbSet<BoardGame>? BoardGame { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<BoardGame>().HasData(
                new BoardGame
                {
                    Id = 1,
                    Name = "Jogo do pao",
                    Price = 0,
                    Game = "jogo"
                });
        }
    }
}
