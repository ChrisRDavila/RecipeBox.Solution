namespace RecipeBox.Models
{
  public class CategoryRecipe
  {       
    public int CategoryRecipeId { get; set; }
    public int RecipeId { get; set; }
    public int CategoryId { get; set; }
    public Recipe Recipe { get; set; }
    public Category Category { get; set; }
  }
}