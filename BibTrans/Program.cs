using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BibTrans.Areas.Identity.Data;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BibTransContextConnection") ?? throw new InvalidOperationException("Connection string 'BibTransContextConnection' not found.");

builder.Services.AddDbContext<BibTransContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<BibTransUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<BibTransContext>();

// Add services to the container.
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication();;

app.UseAuthorization();

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
