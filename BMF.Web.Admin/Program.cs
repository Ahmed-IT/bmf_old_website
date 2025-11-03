using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using BMF.Web.Admin.Database;

// Removed SystemWebAdapters usage and legacy MVC registration; adding Core equivalents

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddScoped<BMFDbContext>(_ => new BMFDbContext("name=BMFDbContext"));
builder.Services.AddControllersWithViews();

var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseStatusCodePagesWithReExecute("/Home/StatusErrorCode", "?code={0}");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

// Conventional route mapping replacing RouteConfig + Global.asax logic
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();