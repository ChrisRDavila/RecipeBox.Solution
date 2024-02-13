using Microsoft.AspNetCore.Builder;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using RecipeBox.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore.Internal;

namespace RecipeBox
{
  class Program
  {
    public static async void Main(string[] args)
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
        var userManager = scope.ServiceProvider.GetRequiredService<UserManager<ApplicationUser>>();
        string email = "admin@admin.com";
        string password = "password#1";

        if(await userManager.FindByEmailAsync(email) == null)
        {
          var user = new ApplicationUser();
          user.UserName = email;
          user.Email = email;
          await userManager.CreateAsync(user, password);
        }
      }

      app.Run();
    }
  }
}