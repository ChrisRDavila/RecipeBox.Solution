System.Collections.Generic;

namespace RecipeBox.Models
{
  public class Recipe
  {
    public int RecipeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    public int Rating { get; set; }
  }
}

//property options
// List or string ingredients, will need for search query later
//List or string directions
//Cooktime
//Serving size
//url link to recipe?
