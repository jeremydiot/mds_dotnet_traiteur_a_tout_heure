using Microsoft.EntityFrameworkCore;

// #############
// init database
// #############

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<OrderDbContext>(opt => opt.UseInMemoryDatabase("OrderDb"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

// ########################
// add fixtures to database
// ########################

var orderContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<OrderDbContext>();
DbInitializer.Initialize(orderContext);


// ######
// Routes
// ######

// get client
app.MapGet("/client/{id}", async (int id, OrderDbContext db) =>
{
  var client = await db.Clients.Include(c => c.Orders).FirstOrDefaultAsync(c => c.Id == id);
  if (!(client is Client)) return Results.NotFound();

  return Results.Ok(client);
});

// get order
app.MapGet("/order/{id}", async (int id, OrderDbContext db) =>
{
  var order = await db.Orders.FirstOrDefaultAsync(o => o.Id == id);
  if (!(order is Order)) return Results.NotFound();
  return Results.Ok(order);
});

// add client order
app.MapPost("/order", async (Order order, OrderDbContext db) =>
{

  int[] recipeIds = { };
  List<Recipe>? recipes = new List<Recipe>();
  double price = .0;
  Dictionary<int, int> ingredientQuantities = new Dictionary<int, int>();

  IngredientService.Purge();
  RecipeService.Purge();

  try
  {
    recipeIds = order.RecipeId?.Split(",").Select(id => int.Parse(id)).ToArray()!;
  }
  catch (System.Exception)
  {
    return Results.BadRequest("recipeId must be a integer");
  }


  foreach (var id in recipeIds)
  {
    var recipe = await RecipeService.Get(id);
    if (recipe != null) recipes.Add(recipe);
  }


  // control quantity et calcul price
  foreach (var recipe in recipes)
  {
    foreach (var recipeIngredient in recipe.RecipeIngredients)
    {
      var ingredient = await IngredientService.Get(recipeIngredient["ingredientId"]);

      int currentQuantity = 0;
      ingredientQuantities.TryGetValue(recipeIngredient["ingredientId"], out currentQuantity);
      currentQuantity += recipeIngredient["ingredientQuantity"];
      ingredientQuantities[recipeIngredient["ingredientId"]] = currentQuantity;

      if (ingredient != null) price += recipeIngredient["ingredientQuantity"] * ingredient.Price;
    }
  }

  // check ingredient disponibility
  foreach (var dictQuantity in ingredientQuantities)
  {
    var ingredient = await IngredientService.Get(dictQuantity.Key);
    if (ingredient != null && dictQuantity.Value > ingredient.Quantity)
    {
      return Results.BadRequest("insufficient quantity for '" + ingredient.Label + "' ingredient, need : " + dictQuantity.Value + ", stock : " + ingredient.Quantity);
    }
  }

  // remove ingredients
  foreach (var dictQuantity in ingredientQuantities)
  {
    await IngredientService.RemoveQuantity(dictQuantity.Key, dictQuantity.Value);
  }


  order.Price = Math.Round(price, 2);
  db.Orders.Add(order);
  await db.SaveChangesAsync();
  return Results.Ok(order);
});

// #################
// Start http server
// #################

app.Run(Environment.GetEnvironmentVariable("API_ORDER_URL") ?? "http://localhost:5002/");
