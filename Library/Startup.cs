using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
// using Library.Data;
using Library.Models;

namespace Library
{
  public class Startup
  {
    public Startup(IWebHostEnvironment env)
    {
      var builder = new ConfigurationBuilder()
          .SetBasePath(env.ContentRootPath)
          .AddJsonFile("appsettings.json");
      Configuration = builder.Build();
    }

    public IConfigurationRoot Configuration { get; set; }

    public void ConfigureServices(IServiceCollection services)
    {
      services.AddMvc();

      services.AddEntityFrameworkMySql()
        .AddDbContext<LibraryContext>(options => options
        .UseMySql(Configuration["ConnectionStrings:DefaultConnection"], ServerVersion.AutoDetect(Configuration["ConnectionStrings:DefaultConnection"])));

      services.AddIdentity<ApplicationUser, IdentityRole>()
            .AddEntityFrameworkStores<LibraryContext>()
            .AddDefaultTokenProviders();
   
    }

    public void Configure(IApplicationBuilder app)
    {
      app.UseDeveloperExceptionPage();
      app.UseAuthentication(); 
      app.UseRouting();
      app.UseAuthorization();

      app.UseEndpoints(routes =>
      {
        routes.MapControllerRoute("default", "{controller=Home}/{action=Admin}/{id?}");
      });

      // app.UseStaticFiles();
      
      // app.Run(async (context) =>
      // {
      //   await context.Response.WriteAsync("Oops! Something went wrong. Please return to previous page.");
      // });

      // CreateRoles(serviceProvider).Wait();
    }

    private async Task CreateRoles(IServiceProvider serviceProvider)
    {
      var RoleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole>>();
      var UserManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
      string[] roleNames = { "Admin", "Librarian", "Patron" };
      IdentityResult roleResult;

      foreach (var roleName in roleNames)
      {
          var roleExist = await RoleManager.RoleExistsAsync(roleName);
          if (!roleExist)
          {
              roleResult = await RoleManager.CreateAsync(new IdentityRole(roleName));
          }
      }

      var _admin = await UserManager.FindByEmailAsync("admin@admin.com");
      if (_admin == null)
      {
          var admin = new ApplicationUser
          {
              UserName = "admin@admin.com",
              Email = "admin@admin.com"
          };

          var createAdmin = await UserManager.CreateAsync(admin, "Admin2022!");
          if (createAdmin.Succeeded)
              await UserManager.AddToRoleAsync(admin, "Admin");
      }

      var _librarian = await UserManager.FindByEmailAsync("librarian@librarian.com");
      if (_librarian == null)
      {
          var librarian = new ApplicationUser
          {
              UserName = "librarian@librarian.com",
              Email = "librarian@librarian.com"
          };

          var createLibrarian = await UserManager.CreateAsync(librarian, "Librarian2022!");
          if (createLibrarian.Succeeded)
              await UserManager.AddToRoleAsync(librarian, "Librarian");
      }

      var _patron = await UserManager.FindByEmailAsync("patron@patron.com");
      if (_patron == null)
      {
          var patron = new ApplicationUser
          {
              UserName = "patron@patron.com",
              Email = "patron@patron.com"
          };

          var createPatron = await UserManager.CreateAsync(patron, "Patron2022!");
          if (createPatron.Succeeded)
              await UserManager.AddToRoleAsync(patron, "Patron");
      }

      // app.UseStaticFiles();

      // app.Run(async (context) =>
      // {
      //   await context.Response.WriteAsync("Oops! Something went wrong. Please return to previous page.");
      // });

      // CreateRoles(serviceProvider).Wait();


    }


  }
}