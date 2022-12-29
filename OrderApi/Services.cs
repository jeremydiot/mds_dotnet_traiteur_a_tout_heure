using System.Net.Http.Headers;
using Newtonsoft.Json;


public class Recipe
{
  public int Id { get; set; }
  public string? Name { get; set; }
  public string? Description { get; set; }
  public Dictionary<string, int>[] RecipeIngredients { get; set; } = { };
}

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

public static class RecipeService
{

  static string RECIPE_API_URL = "http://localhost:5001/recipe/";
  static HttpClient recipeApiClient = new HttpClient();
  static List<Recipe> cache = new List<Recipe>();
  static bool isInitialized = false;

  private static void Init()
  {
    isInitialized = true;
    recipeApiClient.BaseAddress = new Uri(RECIPE_API_URL);
    recipeApiClient.DefaultRequestHeaders.Accept.Clear();
    recipeApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
  }

  public static void Purge()
  {
    cache = new List<Recipe>();
  }

  public static async Task<Recipe?> Get(int recipeId)
  {
    if (!isInitialized) Init();

    Recipe? cacheRecipe = cache.FirstOrDefault(r => r?.Id == recipeId, null);
    if (cacheRecipe != null) return cacheRecipe;

    HttpResponseMessage response = await recipeApiClient.GetAsync(recipeId.ToString());
    if (response.IsSuccessStatusCode)
    {
      var responseData = await response.Content.ReadAsStringAsync();
      var recipe = JsonConvert.DeserializeObject<Recipe>(responseData);
      if (recipe != null) cache.Add(recipe);
      return recipe;
    }
    return null;
  }
}

public static class IngredientService
{
  static string INGREDIENT_API_URL = "http://localhost:5000/ingredient/";
  static HttpClient stockApiClient = new HttpClient();
  static List<Ingredient> cache = new List<Ingredient>();
  static bool isInitialized = false;

  private static void Init()
  {
    isInitialized = true;
    stockApiClient.BaseAddress = new Uri(INGREDIENT_API_URL);
    stockApiClient.DefaultRequestHeaders.Accept.Clear();
    stockApiClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
  }

  public static void Purge()
  {
    cache = new List<Ingredient>();
  }

  public static async Task<Ingredient?> Get(int ingredientId)
  {
    if (!isInitialized) Init();

    Ingredient? cacheIngredient = cache.FirstOrDefault(i => i?.Id == ingredientId, null);
    if (cacheIngredient != null) return cacheIngredient;


    HttpResponseMessage response = await stockApiClient.GetAsync(ingredientId.ToString());
    if (response.IsSuccessStatusCode)
    {
      var responseData = await response.Content.ReadAsStringAsync();
      var ingredient = JsonConvert.DeserializeObject<Ingredient>(responseData);
      if (ingredient != null) cache.Add(ingredient);
      return ingredient;
    }
    return null;
  }

  public static async Task<Ingredient?> RemoveQuantity(int ingredientId, int quantity)
  {
    if (!isInitialized) Init();

    HttpResponseMessage response = await stockApiClient.PutAsJsonAsync(ingredientId.ToString(), new PutIngredient { Operation = "remove", Quantity = quantity });
    if (response.IsSuccessStatusCode)
    {
      var responseData = await response.Content.ReadAsStringAsync();
      var ingredient = JsonConvert.DeserializeObject<Ingredient>(responseData);
      return ingredient;
    }
    return null;

  }
}

