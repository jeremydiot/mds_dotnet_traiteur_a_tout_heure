// #################################
// Tables structures and data models
// #################################

public class Ingredient
{
  public int Id { get; set; }
  public double Price { get; set; }
  public string? Label { get; set; }
  public int Quantity { get; set; }
}

public class PutIngredient
{
  public string? Operation { get; set; }
  public int Quantity { get; set; }
}