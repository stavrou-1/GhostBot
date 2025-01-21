using System.Configuration;
using System.Linq;
using Autofac;
using GhostBot.DataContext;
using GhostBot.EntityModels;

#region DB Context Setup
// string projectPath = AppDomain.CurrentDomain.BaseDirectory.Split(new String[] { @"bin\" }, StringSplitOptions.None)[0];
// IConfigurationRoot configuration = new ConfigurationBuilder()
//     .SetBasePath(projectPath)
//     .AddJsonFile("appsettings.json")
//     .Build();
// string connectionString = configuration.GetConnectionString("DefaultConnection");
// //db context setup.
// WriteLine (connectionString);
// ReadLine ();

// using GhostBotContext db = new GhostBotContext(connectionString);
// WriteLine ($"Provider: {db.Database.ProviderName}");
// IQueryable<Category> ? categories = db.Category;
// if (categories is not null) {
//     foreach (Category c in categories) {
//         WriteLine ($"ID: {c.CategoryId}, Name: {c.CategoryName}");
//     }
// }

// IQueryable<Person> ? persons = db.Person;
// if (persons is not null) {
//     foreach (Person c in persons) {
//         WriteLine ($"ID: {c.PersonId}, FirstName: {c.FirstName}, LastName: {c.LastName}");
//     }
// }

// IQueryable<Comment> ? comments = db.Comment;
// if (comments is not null) {
//     foreach (Comment c in comments) {
//         WriteLine ($"ID: {c.CommentId}, Content: {c.Content}, PersonId: {c.PersonId}");
//     }
// }

ReadLine ();
#endregion  

#region Dependency Injection Setup
// ContainerBuilder containerBuilder = new ContainerBuilder ();
// containerBuilder.RegisterType<SMSService>().As<IMobileService>();
// containerBuilder.RegisterType<EmailService>().As<IMailService>();

// IContainer container = containerBuilder.Build ();
// container.Resolve<IMobileService>().Execute();
// container.Resolve<IMobileService>().Execute();
#endregion

#region Configure the web server host and services
var builder = WebApplication.CreateBuilder (args);

// Add services to the container.
var connectionString = builder.Configuration.GetConnectionString ("DefaultConnection") ?? "";
builder.Services.AddRazorPages ();
builder.Services.AddControllersWithViews ();
#endregion

#region Configure HTTP Pipeline and Middleware
var app = builder.Build ();
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