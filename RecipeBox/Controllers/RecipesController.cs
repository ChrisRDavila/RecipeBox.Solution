using RecipeBox.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using System.Threading.Tasks;
using System.Security.Claims;

namespace RecipeBox.Controllers
{
  [Authorize(Roles = "Admin, User")]
  public class RecipesController : Controller
  {
    private readonly RecipeBoxContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public RecipesController(UserManager<ApplicationUser> userManager,RecipeBoxContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    [AllowAnonymous]
    public async Task<ActionResult> Index()
    {
      ViewBag.PageTitle = "Recipes";
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      List<Recipe> userRecipe = _db.Recipes
                    .Where(entry => entry.User.Id == currentUser.Id)
                    .ToList();
      return View(userRecipe);
    }


    public ActionResult Create()
    {
      ViewBag.PageTitle = "Add a Recipe";
      return View();
    }

    [HttpPost]
    public async Task<ActionResult> Create(Recipe Recipe)
    {
      if (!ModelState.IsValid)
      {
          ViewBag.PageTitle = "Add a Recipe";
          return View(Recipe);
      }
      else
      {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        Recipe.User = currentUser;
        _db.Recipes.Add(Recipe);
        _db.SaveChanges();
        return View("Index");
      }
    }
    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      Recipe thisRecipe = _db.Recipes
          .Include(rcp => rcp.JoinEntities)
          .ThenInclude(join => join.Category)
          .FirstOrDefault(rcp => rcp.RecipeId == id);
      ViewBag.PageTitle = $"Recipe Details - {thisRecipe.Name} ";
      return View(thisRecipe);
    }

    public ActionResult Edit(int id)
    {
      Recipe thisRecipe = _db.Recipes.FirstOrDefault(rcp => rcp.RecipeId == id);
      ViewBag.PageTitle = $"Edit Recipe - {thisRecipe.Name}";
      return View(thisRecipe);
    }

    [HttpPost]
    public ActionResult Edit(Recipe rcp)
    {
      if (!ModelState.IsValid)
      {
          ViewBag.PageTitle = $"Edit Recipe - {rcp.Name}";
          return View(rcp);
      }
      else
      {
      _db.Recipes.Update(rcp);
      _db.SaveChanges();
      return RedirectToAction("Index");
      }
    }

    public ActionResult Delete(int id)
    {
      Recipe thisRecipe = _db.Recipes.FirstOrDefault(rcp => rcp.RecipeId == id);
      ViewBag.PageTitle = $"Delete Recipe - {thisRecipe.Name}";
      return View(thisRecipe);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Recipe thisRecipe = _db.Recipes.FirstOrDefault(rcp => rcp.RecipeId == id);
      _db.Recipes.Remove(thisRecipe);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public async Task<ActionResult> AddCategory(int id)
    {
      Recipe thisRecipe = _db.Recipes
        .Include(rcp => rcp.JoinEntities)
        .FirstOrDefault(rcp => rcp.RecipeId == id);
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      List<Category> userCategories = _db.Categories
        .Where(entry => entry.User.Id == currentUser.Id)
        .ToList();
      ViewBag.CategoryId = new SelectList(_db.Categories.Where(entry => entry.User.Id == currentUser.Id), "CategoryId", "Type");
      ViewBag.PageTitle = $"Link {thisRecipe.Name} To Category";
      return View(thisRecipe);
    }

    [HttpPost]
    public ActionResult AddCategory(Recipe rcp, int categoryId)
    {
      #nullable enable
      CategoryRecipe? joinEntity = _db.CategoryRecipes.FirstOrDefault(join => (join.CategoryId == categoryId && join.RecipeId == rcp.RecipeId));
      #nullable disable
      if (categoryId != 0 && joinEntity == null)
      {
        CategoryRecipe linkedRecipe = new CategoryRecipe() { CategoryId = categoryId, RecipeId = rcp.RecipeId};
        _db.CategoryRecipes.Add(linkedRecipe);
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = rcp.RecipeId });
    }

    [HttpPost]
    public ActionResult DeleteRecipeLink(int categoryRecipeId)
    {
      CategoryRecipe recipeLink = _db.CategoryRecipes.FirstOrDefault(entry => entry.CategoryRecipeId == categoryRecipeId);
      _db.CategoryRecipes.Remove(recipeLink);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }
  }
}


// [AllowAnonymous]
//     public async Task<ActionResult> Index()
//     {
//       if (User.Identity.IsAuthenticated)
//       {
//         ViewBag.PageTitle = "Recipes";
//         string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
//         ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
//         List<Recipe> userRecipe = _db.Recipes
//                       .Where(entry => entry.User.Id == currentUser.Id)
//                       .ToList();
//         return View(userRecipe);
//       }
//       else
//       {
//         List<Recipe> model = _db.Recipes.ToList();
//         return View(model);
//       }
//     }