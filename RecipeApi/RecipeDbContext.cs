using Microsoft.EntityFrameworkCore;

public class RecipeDbContext : DbContext
{
  public RecipeDbContext(DbContextOptions<RecipeDbContext> options) : base(options) { }
  public DbSet<Recipe> Recipes => Set<Recipe>();
  public DbSet<RecipeIngredient> RecipeIngredients => Set<RecipeIngredient>();

  protected override void OnModelCreating(ModelBuilder modelBuilder)
  {
    modelBuilder.Entity<Recipe>().HasMany(r => r.RecipeIngredients).WithOne().HasForeignKey(r => r.RecipeId);
    modelBuilder.Entity<RecipeIngredient>().HasIndex(r => new { r.RecipeId, r.IngredientId }).IsUnique(true);
  }
}
