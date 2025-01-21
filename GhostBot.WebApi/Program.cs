using GhostBot.DataContext;
using GhostBot.WebApi.Repositories;
using Microsoft.AspNetCore.Mvc.Formatters;
using Microsoft.Extensions.Caching.Memory;
 //To use IPersonRepository
using Microsoft.AspNetCore.HttpLogging; //To use HttpLoggingFields
using Swashbuckle.AspNetCore.SwaggerUI;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

//configure logging.
builder.Services.AddHttpLogging(options => {
    options.LoggingFields = HttpLoggingFields.All;
    options.RequestBodyLogLimit = 4096; //default is 32k
    options.ResponseBodyLogLimit = 4096; //default is 32k
});

//Using W3C Logging will reduce performance of the app.
builder.Services.AddW3CLogging(options => {
    options.AdditionalRequestHeaders.Add("x-forwarded-for");
    options.AdditionalRequestHeaders.Add("x-client-ssl-protocol");
});

// Add services to the container.
builder.Services.AddSingleton<IMemoryCache>(new MemoryCache(new MemoryCacheOptions()));
builder.Services.AddGhostBotContext();
builder.Services.AddControllers(options =>
{
    WriteLine("Default output formatters.");
    foreach (IOutputFormatter formatter in options.OutputFormatters)
    {
        OutputFormatter? mediaFormatter = formatter as OutputFormatter;
        if (mediaFormatter is null)
        {
            WriteLine($"{formatter.GetType().Name}");
        }
        else
        {
            WriteLine("   {0},   Media types: {1}",
            arg0: mediaFormatter.GetType().Name,
            arg1: string.Join(", ", mediaFormatter.SupportedMediaTypes));
        }
    }
})
.AddXmlDataContractSerializerFormatters()
.AddXmlSerializerFormatters();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//register scoped dependencies
builder.Services.AddScoped<IPersonRepository, PersonRepository>();
var app = builder.Build();

app.UseHttpLogging();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment()) {
    app.UseSwagger ();
    app.UseSwaggerUI(c => {
        c.SwaggerEndpoint("/swagger/v1/swagger.json",
        "GhostBot Service API Version 1");

        c.SupportedSubmitMethods(new[] {
            SubmitMethod.Get, SubmitMethod.Post,
            SubmitMethod.Put, SubmitMethod.Delete
        });
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();