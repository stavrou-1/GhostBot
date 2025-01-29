using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using GhostBot.EntityModels;
using GhostBot.DataContext;
using System.Net.Http.Headers; // To use MediaTypeWithQualityHeaderValue.
using System.Net;
using GhostBot.Mvc.Data;

#region Configure the web server host and services
var builder = WebApplication.CreateBuilder (args);

// Add services to the container.
var connectionString = builder.Configuration
    .GetConnectionString ("DefaultConnection") ??
        throw new InvalidOperationException(
            "Connection string 'DefaultConnection' not found.");
    
builder.Services.AddDbContext<ApplicationDbContext>(options => 
    options.UseSqlite(connectionString)
);
builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
builder.Services.AddGhostBotContext();
//if you are using sqlite, de
#endregion

#region Configure HTTP Pipeline and Middleware
builder.Services.AddHttpClient(name: "GhostBot.WebApi",
configureClient: options => 
{
    options.DefaultRequestVersion = HttpVersion.Version30;
    options.DefaultVersionPolicy = HttpVersionPolicy.RequestVersionExact;
    options.BaseAddress = new Uri("http://localhost:5019/");
    options.DefaultRequestHeaders.Accept.Add(
        new MediaTypeWithQualityHeaderValue(
            mediaType: "application/json", quality: 1.0
        ));
});

var app = builder.Build();
// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment ()) {
    app.UseExceptionHandler ("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts ();
}
app.UseHttpsRedirection ();
app.UseStaticFiles ();
app.UseRouting ();
app.UseAuthorization ();
app.MapControllerRoute (
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");
app.MapGet ("/hidden", () => "hidden page!");
app.Run ();
#endregion