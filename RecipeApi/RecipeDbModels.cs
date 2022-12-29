public class Recipe
{
  public int Id { get; set; }
  public string? Name { get; set; }
  public string? Description { get; set; }
  public virtual ICollection<RecipeIngredient>? RecipeIngredients { get; set; }
}

public class RecipeIngredient
{
  public int Id { get; set; }
  public int RecipeId { get; set; }
  // public Recipe? Recipe { get; set; }
  public int IngredientId { get; set; }
  public int IngredientQuantity { get; set; }
}