using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;

namespace RecipeBox.Controllers
{
    public class HomeController : Controller
    {
      private readonly RecipeBoxContext _db;

      public HomeController(RecipeBoxContext db)
      {
        _db = db;
      }

      [HttpGet("/")]
      public ActionResult Index()
      {
        Category[] cats = _db.Categories.OrderBy(categories => categories.Type).ToArray();
        Recipe[] recipes = _db.Recipes.OrderByDescending(recipes => recipes.Rating).ToArray();
        Dictionary<string,object[]> model = new Dictionary<string, object[]>();
        model.Add("categories", cats);
        model.Add("recipes", recipes);
        @ViewBag.PageTitle = "Home";
        return View(model);
      }
    }
}