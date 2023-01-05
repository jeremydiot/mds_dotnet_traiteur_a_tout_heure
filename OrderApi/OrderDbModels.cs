// #################################
// Tables structures and data models
// #################################

public class Order
{
  public int Id { get; set; }
  public double Price { get; set; }
  public string? RecipeId { get; set; }
  public int ClientId { get; set; }
  // public Client? Client { get; set; }
}

public class Client
{
  public int Id { get; set; }
  public string? Lastname { get; set; }
  public string? Firstname { get; set; }
  public string? Address { get; set; }
  public virtual ICollection<Order>? Orders { get; set; }
}