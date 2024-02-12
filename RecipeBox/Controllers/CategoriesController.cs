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
  [Authorize]
  public class CategoriesController : Controller
  {
    private readonly RecipeBoxContext _db;
    private readonly UserManager<ApplicationUser> _userManager;

    public CategoriesController(UserManager<ApplicationUser> userManager, RecipeBoxContext db)
    {
      _userManager = userManager;
      _db = db;
    }

    [AllowAnonymous]
    public async Task<ActionResult> Index()
    {
      ViewBag.PageTitle = "Categories";
      string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
      ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
      List<Category> userCategory = _db.Categories
                .Where(entry => entry.User.Id == currentUser.Id)
                .ToList();
      return View(userCategory);
    }

    public ActionResult Create()
    {
      ViewBag.PageTitle = "Add a Category";
      return View();
    }
    
    [HttpPost]
    public async Task<ActionResult> Create(Category Category)
    {
      if (!ModelState.IsValid)
      {
          ViewBag.PageTitle = "Add a Category";
          return View(Category);
      }
      else
      {
        string userId = User.FindFirst(ClaimTypes.NameIdentifier)?.Value;
        ApplicationUser currentUser = await _userManager.FindByIdAsync(userId);
        Category.User = currentUser;
        _db.Categories.Add(Category);
        _db.SaveChanges();
        return RedirectToAction("Index");
      }
    }

    [AllowAnonymous]
    public ActionResult Details(int id)
    {
      Category thisCategory = _db.Categories
          .Include(ctg => ctg.JoinEntities)
          .ThenInclude(join => join.Recipe)
          .FirstOrDefault(ctg => ctg.CategoryId == id);
      ViewBag.PageTitle = $"Category Details - {thisCategory.Type} ";
      ViewBag.RecipesCount = _db.Recipes.Count();
      return View(thisCategory);
    }

    public ActionResult Edit(int id)
    {
      Category thisCategory = _db.Categories.FirstOrDefault(ctg => ctg.CategoryId == id);
      ViewBag.PageTitle = $"Edit Category - {thisCategory.Type}";
      return View(thisCategory);
    }

    [HttpPost]
    public ActionResult Edit(Category ctg)
    {
      if (!ModelState.IsValid)
      {
          ViewBag.PageTitle = $"Edit Category - {ctg.Type}";
          return View(ctg);
      }
      else
      {
      _db.Categories.Update(ctg);
      _db.SaveChanges();
      return RedirectToAction("Index");
      }
    }

    public ActionResult Delete(int id)
    {
      Category thisCategory = _db.Categories.FirstOrDefault(ctg => ctg.CategoryId == id);
      ViewBag.PageTitle = $"Delete Category - {thisCategory.Type}";
      return View(thisCategory);
    }

    [HttpPost, ActionName("Delete")]
    public ActionResult DeleteConfirmed(int id)
    {
      Category thisCategory = _db.Categories.FirstOrDefault(ctg => ctg.CategoryId == id);
      _db.Categories.Remove(thisCategory);
      _db.SaveChanges();
      return RedirectToAction("Index");
    }

    public ActionResult AddRecipe(int id)
    {
      Category thisCategory = _db.Categories
        .Include(ctg => ctg.JoinEntities)
        .FirstOrDefault(ctg => ctg.CategoryId == id);
      ViewBag.RecipeId = new SelectList(_db.Recipes, "RecipeId", "RecipeName");
      ViewBag.PageTitle = $"Link {thisCategory.Type} To Recipe";
      return View(thisCategory);
    }

    [HttpPost]
    public ActionResult AddRecipe(Category ctg, int recipeId)
    {
      #nullable enable
      CategoryRecipe? joinEntity = _db.CategoryRecipes.FirstOrDefault(join => (join.RecipeId == recipeId && join.CategoryId == ctg.CategoryId));
      #nullable disable
      if (recipeId != 0 && joinEntity == null)
      {
        CategoryRecipe linkedRecipe = new CategoryRecipe() { RecipeId = recipeId, CategoryId = ctg.CategoryId};
        _db.CategoryRecipes.Add(linkedRecipe);
        _db.SaveChanges();
      }
      return RedirectToAction("Details", new { id = ctg.CategoryId });
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