using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecipeBox.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;
using System.Threading.Tasks;

namespace RecipeBox
{
  class Program
  {
    public static async Task Main(string[] args)
    {

      WebApplicationBuilder builder = WebApplication.CreateBuilder(args);

      builder.Services.AddControllersWithViews();

      builder.Services.AddDbContext<RecipeBoxContext>(
                        dbContextOptions => dbContextOptions
                          .UseMySql(
                            builder.Configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(builder.Configuration["ConnectionStrings:DefaultConnection"]
                          )
                        )
                      );

      builder.Services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddRoles<IdentityRole>()
                .AddEntityFrameworkStores<RecipeBoxContext>()
                .AddDefaultTokenProviders();

      WebApplication app = builder.Build();

      // app.UseDeveloperExceptionPage();
      app.UseHttpsRedirection();
      app.UseStaticFiles();

      app.UseRouting();

      app.UseAuthentication();
      app.UseAuthorization();

      app.MapControllerRoute(
          name: "default",
          pattern: "{controller=Home}/{action=Index}/{id?}"
        );

      using (var scope = app.Services.CreateScope())
      {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole>>();

        var roles = new[] { "Admin", "User" };
        foreach (var role in roles)
        {
          if (!await roleManager.RoleExistsAsync(role))
            await roleManager.CreateAsync(new IdentityRole(role));
        }
      }

      using (var scope = app.Services.CreateScope())
      {
        var _userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        string email = "admin@admin.com";
        string password = "Password#1";

        if (await _userManager.FindByEmailAsync(email) == null)
        {
          var User = new ApplicationUser();
          User.UserName = email;
          User.Email = email;
          await _userManager.CreateAsync(User, password);
          await _userManager.AddToRoleAsync(User, "Admin");
        }
      }

      app.Run();
    }
  }
}