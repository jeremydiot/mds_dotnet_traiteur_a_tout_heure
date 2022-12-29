using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<StockDbContext>(opt => opt.UseInMemoryDatabase("StockDb"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();

var app = builder.Build();

// add fixtures
var context = app.Services.CreateScope().ServiceProvider.GetRequiredService<StockDbContext>();
DbInitializer.Initialize(context);


app.MapGet("/ingredient/{id}", async (int id, StockDbContext db) =>
 await db.Ingredients.FindAsync(id)
  is Ingredient ingredient
    ? Results.Ok(ingredient)
    : Results.NotFound());


app.MapPut("/ingredient/{id}", async (int id, PutIngredient inputIngredient, StockDbContext db) =>
{
  var ingredient = await db.Ingredients.FindAsync(id);
  if (ingredient is null) return Results.NotFound();

  var dbQuantity = ingredient.Quantity;

  if (inputIngredient.Operation == "add") dbQuantity += inputIngredient.Quantity;
  else if (inputIngredient.Operation == "remove") dbQuantity -= inputIngredient.Quantity;
  else return Results.BadRequest("unknown operation, only 'add' or 'remove' are permited");

  if (dbQuantity < 0) return Results.BadRequest("insufficient quantity");
  else ingredient.Quantity = dbQuantity;

  await db.SaveChangesAsync();
  return Results.Ok(ingredient);
});

app.Run("http://localhost:5000/");