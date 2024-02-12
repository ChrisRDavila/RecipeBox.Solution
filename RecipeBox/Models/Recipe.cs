using System.Collections.Generic;

namespace RecipeBox.Models
{
  public class Recipe
  {
    public int RecipeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Rating { get; set; }
    public string Ingredients { get; set; }
    public string Directions { get; set; }
    public float CookTime { get; set; }
    public float ServingSize { get; set; }
    public string UrlLink { get; set; }
    public List<CategoryRecipe> JoinEntities { get; set; }
    public ApplicationUser User { get; set; }
  }
}
