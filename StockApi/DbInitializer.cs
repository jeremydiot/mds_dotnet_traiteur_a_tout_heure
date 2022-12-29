public static class DbInitializer
{
  public static void Initialize(StockDbContext context)
  {
    context.Database.EnsureCreated();

    var ingredients = new Ingredient[]{
      new Ingredient{Id=1,Label="tomate", Price=0.5, Quantity=20},
      new Ingredient{Id=2,Label="aubergine", Price=0.7, Quantity=20},
      new Ingredient{Id=3,Label="poivron", Price=0.2, Quantity=20},
      new Ingredient{Id=4,Label="courgette", Price=0.2, Quantity=20},
      new Ingredient{Id=5,Label="oignon", Price=0.1, Quantity=20},
      new Ingredient{Id=6,Label="ail", Price=0.1, Quantity=20},
      new Ingredient{Id=7,Label="banane", Price=0.5, Quantity=20},
      new Ingredient{Id=8,Label="fraise", Price=0.3, Quantity=20},
      new Ingredient{Id=9,Label="orange", Price=0.5, Quantity=20},
      new Ingredient{Id=10,Label="pomme", Price=0.2, Quantity=20},
      new Ingredient{Id=11,Label="ananas", Price=1, Quantity=20},
      new Ingredient{Id=12,Label="mangue", Price=3, Quantity=20},
      new Ingredient{Id=13,Label="kiwi", Price=0.8, Quantity=20},
    };

    foreach (Ingredient i in ingredients)
    {
      context.Ingredients.Add(i);
    }

    context.SaveChanges();
  }
}