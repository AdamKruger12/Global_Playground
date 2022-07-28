using Microsoft.EntityFrameworkCore;

namespace ShiftTech.Models.Context
{
  public class CardContext : DbContext
  {
    public CardContext(DbContextOptions<CardContext> options) : base(options) { }
    public DbSet<Card>? Cards { get; set; }
    public DbSet<Company>? Company { get; set; }
  }
}
