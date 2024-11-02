using BibTrans.Areas.Identity.Data;
using Microsoft.EntityFrameworkCore;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BibTransContextConnection") ?? throw new InvalidOperationException("Connection string 'BibTransContextConnection' not found.");

builder.Services.AddDbContext<BibTransContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<BibTransUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<BibTransContext>();

// Add services to the container.
builder.Services.AddControllersWithViews()
.AddJsonOptions(options =>
{
    // A property naming policy, or null to leave property names unchanged.
    options.JsonSerializerOptions.PropertyNamingPolicy = null;
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
}
app.UseStaticFiles();

app.UseRouting();
app.UseAuthentication(); ;

app.UseAuthorization();

app.MapControllerRoute(
    name: "Admin",
    pattern: "{area:exists}/{controller=Home}/{action=Index}/{id?}");

app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
