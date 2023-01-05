using Microsoft.EntityFrameworkCore;

// #############
// init database
// #############

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<RecipeDbContext>(opt => opt.UseInMemoryDatabase("RecipeDb"));
builder.Services.AddDatabaseDeveloperPageExceptionFilter();
var app = builder.Build();

// ########################
// add fixtures to database
// ########################

var recipeContext = app.Services.CreateScope().ServiceProvider.GetRequiredService<RecipeDbContext>();
DbInitializer.Initialize(recipeContext);

// ######
// Routes
// ######

// get recipe
app.MapGet("/recipe/{id}", async (int id, RecipeDbContext db) =>
{
  var recipe = await db.Recipes.Include(r => r.RecipeIngredients).FirstOrDefaultAsync(r => r.Id == id);
  if (!(recipe is Recipe)) return Results.NotFound();

  return Results.Ok(recipe);
});

// #################
// Start http server
// #################

app.Run(Environment.GetEnvironmentVariable("API_RECIPE_URL") ?? "http://localhost:5001/");