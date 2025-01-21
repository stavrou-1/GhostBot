using System.Net.Http.Headers; // To use MediaTypeWithQualityHeaderValue.

#region Configure the web server host and services
var builder = WebApplication.CreateBuilder (args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString ("DefaultConnection") ?? "";
builder.Services.AddRazorPages ();
builder.Services.AddControllersWithViews ();
#endregion

#region Configure HTTP Pipeline and Middleware

builder.Services.AddHttpClient(name: "GhostBot.WebApi",
configureClient: options => 
{
    options.BaseAddress = new Uri("https://localhost:5019/");
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