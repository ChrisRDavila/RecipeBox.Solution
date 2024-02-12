using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace RecipeBox.Models
{
  public class RecipeBoxContext : IdentityDbContext<ApplicationUser>
  {
    public DbSet<ClassName> ClassName { get; set; }
    public DbSet<ClassName2> ClassName2 { get; set; }
    public DbSet<JoinTable> Class1Class2s { get; set; }

    public RecipeBoxContext(DbContextOptions options) : base(options) { }
  }
}