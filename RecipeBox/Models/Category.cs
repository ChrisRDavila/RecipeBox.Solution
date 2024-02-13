using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace RecipeBox.Models
{
  public class Category
  {
    public int CategoryId { get; set; }
    [Display(Name = "Region of origin, Cook Method, or base protein")]
    public string Type { get; set; }
    public List<CategoryRecipe> JoinEntities { get; set; }
    public ApplicationUser User { get; set; }
  }
}
