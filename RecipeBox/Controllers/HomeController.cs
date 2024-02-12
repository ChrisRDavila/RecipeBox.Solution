using Microsoft.AspNetCore.Mvc;



namespace RecipeBox.Controllers
{
  public class HomeController : Controller
  {
    [Route("/")]
    // Notice the changes below!
    public ActionResult File() { return View(); }
  }
}