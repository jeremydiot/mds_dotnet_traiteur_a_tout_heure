using Microsoft.EntityFrameworkCore;

public class OrderDbContext : DbContext
{
  public OrderDbContext(DbContextOptions<OrderDbContext> options) : base(options) { }
  public DbSet<Order> Orders => Set<Order>();
  public DbSet<Client> Clients => Set<Client>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Client>().HasMany(c => c.Orders).WithOne().HasForeignKey(o => o.ClientId);
  }
}