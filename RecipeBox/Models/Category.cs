using System.Collections.Generic;

namespace RecipeBox.Models
{
  public class Category
  {
    public int CategoryId { get; set; }
    public string Type { get; set; }
    public string Protein { get; set; }
    public string CookMethod { get; set; }
    public List<CategoryRecipe> JoinEntities { get; set; }
    public ApplicationUser User { get; set; }
  }
}
