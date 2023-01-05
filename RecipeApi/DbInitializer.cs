// #################
// Database fixtures
// #################

public static class DbInitializer
{
  public static void Initialize(RecipeDbContext context)
  {

    context.Database.EnsureCreated();

    var recipes = new Recipe[]{
      new Recipe{Id=1,Name="ratatouille",Description="parfait pour l'hiver"},
      new Recipe{Id=2,Name="salade de fruit",Description="parfait pour l'été"},
    };

    foreach (Recipe r in recipes)
    {
      context.Recipes.Add(r);
    }

    context.SaveChanges();

    var recipeIngredients = new RecipeIngredient[]{
        // ratatouille
        new RecipeIngredient{Id=1, RecipeId=1, IngredientId=1, IngredientQuantity=3},
        new RecipeIngredient{Id=2, RecipeId=1, IngredientId=2, IngredientQuantity=1},
        new RecipeIngredient{Id=3, RecipeId=1, IngredientId=3, IngredientQuantity=2},
        new RecipeIngredient{Id=4, RecipeId=1, IngredientId=4, IngredientQuantity=2},
        new RecipeIngredient{Id=5, RecipeId=1, IngredientId=5, IngredientQuantity=1},
        new RecipeIngredient{Id=6, RecipeId=1, IngredientId=6, IngredientQuantity=2},
        // salade de fruit
        new RecipeIngredient{Id=7, RecipeId=2, IngredientId=7, IngredientQuantity=4},
        new RecipeIngredient{Id=8, RecipeId=2, IngredientId=8, IngredientQuantity=10},
        new RecipeIngredient{Id=9, RecipeId=2, IngredientId=9, IngredientQuantity=2},
        new RecipeIngredient{Id=10, RecipeId=2, IngredientId=10, IngredientQuantity=3},
        new RecipeIngredient{Id=11, RecipeId=2, IngredientId=11, IngredientQuantity=1},
        new RecipeIngredient{Id=12, RecipeId=2, IngredientId=12, IngredientQuantity=1},
        new RecipeIngredient{Id=13, RecipeId=2, IngredientId=13, IngredientQuantity=4},
      };

    foreach (RecipeIngredient r in recipeIngredients)
    {
      context.RecipeIngredients.Add(r);
    }

    context.SaveChanges();
  }

}