using System.Net;
using Autofac;
using GhostBot.Mvc.Interfaces;
using GhostBot.Mvc.Services;


ContainerBuilder containerBuilder = new ContainerBuilder();
containerBuilder.RegisterType<SMSService>().As<IMobileService>();
containerBuilder.RegisterType<EmailService>().As<IMailService>();

IContainer container = containerBuilder.Build ();
container.Resolve<IMobileService>().Execute();
container.Resolve<IMobileService>().Execute();


#region Configure the web server host and services
var builder = WebApplication.CreateBuilder(args);
// Add services to the container.

builder.Services.AddRazorPages();
builder.Services.AddControllersWithViews();
#endregion


var app = builder.Build ();

// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment()) {
    app.UseExceptionHandler("/Home/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseRouting();

app.UseAuthorization();
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");


app.MapGet("/lizards", () => "lizards page");
app.Run();