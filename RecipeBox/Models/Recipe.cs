using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBox.Models
{
  public class Recipe
  {
    public int RecipeId { get; set; }
    public string Name { get; set; }
    public string Description { get; set; }
    [Range(1, 10, ErrorMessage = "Rating must be between 1 and 10")]
    public int Rating { get; set; }
    public string Ingredients { get; set; }
    public string Directions { get; set; }
    [Display(Name = "Cook Time")]
    public string CookTime { get; set; }
    [Display(Name = "Serving Size")]
    public string ServingSize { get; set; }
    [Display(Name = "Recipe Link")]
    public string UrlLink { get; set; }
    public List<CategoryRecipe> JoinEntities { get; set; }
    public ApplicationUser User { get; set; }
  }
}
