using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using BibTrans.Areas.Identity.Data;
using BibTrans.Services;
using Microsoft.AspNetCore.Identity.UI.Services;
var builder = WebApplication.CreateBuilder(args);
var connectionString = builder.Configuration.GetConnectionString("BibTransContextConnection") ?? throw new InvalidOperationException("Connection string 'BibTransContextConnection' not found.");

builder.Services.AddDbContext<BibTransContext>(options =>
    options.UseSqlServer(connectionString));

builder.Services.AddDefaultIdentity<BibTransUser>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<BibTransContext>();

builder.Services.AddTransient<IEmailSender, SendGridEmailSender>(provider =>
   new SendGridEmailSender(""));

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
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.MapRazorPages();

app.Run();
