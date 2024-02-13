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
  public class SearchController : Controller
  {
    private readonly RecipeBoxContext _db;
    public SearchController(RecipeBoxContext db)
    {
      _db = db;
    }

    private async Task<List<Recipe>> DoRecipeSearch(string query)
    {
      IQueryable<Recipe> results = _db.Set<Recipe>();
      if (query != null)
      {
        return await results?.Where(rcp => rcp.Name.Contains(query) || rcp.Description.Contains(query) || rcp.Rating.ToString().Contains(query) || rcp.Ingredients.Contains(query) || rcp.Directions.Contains(query) || rcp.CookTime.Contains(query) || rcp.ServingSize.Contains(query) || rcp.UrlLink.Contains(query)).ToListAsync();
      }
      else
      {
        return await results.ToListAsync();
      }
    }

    private async Task<List<Category>> DoCategorySearch(string query)
    {
      IQueryable<Category> results = _db.Set<Category>();
      if (query != null)
      {
        return await results?.Where(ctg => ctg.Type.Contains(query)).ToListAsync();
      }
      else
      {
        return await results.ToListAsync();
      }
    }

    public async Task<IActionResult> Index(string query)
    {
      ViewBag.PageTitle = $"Search results for {query}";
      List<Category> ctgResults = await DoCategorySearch(query);
      if (ctgResults.Count == 0)
      {
        List<Recipe> rcpResults = await DoRecipeSearch(query);
        @ViewBag.SearchType = "recipes";
        return View(rcpResults);
      }
      else 
      {
        @ViewBag.SearchType = "categories";
        return View(ctgResults);
      }
    }
  }
}

