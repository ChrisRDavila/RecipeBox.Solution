using Microsoft.AspNetCore.Mvc;
using RecipeBox.Models;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;



namespace RecipeBox.Controllers
{
  public class HomeController : Controller
  {
    private readonly RecipeBoxContext _db;
    private readonly UserManager<ApplicationUser> _userManager;
    public HomeController(UserManager<ApplicationUser> userManager, RecipeBoxContext db) 
    {
      _userManager = userManager;
      _db = db;
    }

    [HttpGet("/")]
      public async Task<ActionResult> Index()
      {
        Category[] cats = _db.Categories.ToArray();
        RecipeBox[] rcp = _db.Recipes.ToArray();
        Dictionary<string,object[]> model = new Dictionary<string, object[]>();
        model.Add("categories", cats);
        model.Add("recipes", rcp);
        return View(model);
      }


  }
}

