using Microsoft.EntityFrameworkCore;
using SportsStore.Models;
using SportsStore.Models.Repository;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllersWithViews();

builder.Services.AddDbContext<StoreDbContext>(opts =>
{
    opts.UseSqlServer(builder.Configuration["ConnectionStrings:SportsStoreConnection"]);
});

builder.Services.AddScoped<IStoreRepository, EFStoreRepository>();

builder.Services.AddDistributedMemoryCache();

builder.Services.AddSession();

builder.Services.AddScoped<Cart>(SessionCart.GetCart);

builder.Services.AddSingleton<IHttpContextAccessor, HttpContextAccessor>();

var app = builder.Build();

app.UseStaticFiles();

app.UseSession();

app.MapControllerRoute(
      "categoryPage",
      "Products/{category}/Page{productPage:int}",
      new { Controller = "Home", action = "Index" });

app.MapControllerRoute(
      "shoppingCart",
      "Cart",
      new { Controller = "Cart", action = "Index" });

app.MapControllerRoute(
      "category",
      "{category}",
      new { Controller = "Home", action = "Index", productPage = 1 });

app.MapControllerRoute(
    "pagination",
    "Products/Page{productPage}",
    new { Controller = "Home", action = "Index", productPage = 1 });

app.MapControllerRoute(
      "default",
      "/",
      new { Controller = "Home", action = "Index" });

SeedData.EnsurePopulated(app);

app.Run();
