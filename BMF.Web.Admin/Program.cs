using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
// Removed SystemWebAdapters usage and legacy MVC registration; adding Core equivalents

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllersWithViews();

// TODO: Register EF classic context via DI (to be refactored to EF Core later)
// builder.Services.AddScoped<BMFDbContext>(sp => new BMFDbContext("name=BMFDbContext"));

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