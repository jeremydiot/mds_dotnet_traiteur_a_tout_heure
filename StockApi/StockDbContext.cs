// ######################
// Define database tables
// ######################

using Microsoft.EntityFrameworkCore;

public class StockDbContext : DbContext
{
  public StockDbContext(DbContextOptions<StockDbContext> options) : base(options) { }
  public DbSet<Ingredient> Ingredients => Set<Ingredient>();
}