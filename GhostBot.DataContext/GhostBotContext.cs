using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using GhostBot.EntityModels;

namespace GhostBot.DataContext;

public class GhostBotContext : DbContext
{
    public GhostBotContext()
    {   
    }

    public GhostBotContext(DbContextOptions<GhostBotContext> options) : base(options)
    {
    }

    public virtual DbSet<Person>? Person { get; set; }
    public virtual DbSet<Comment>? Comment { get; set; }
    public virtual DbSet<Category>? Category { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        string dataBaseFile = "GhostBot.db";
        string path = Path.Combine(Environment.CurrentDirectory, dataBaseFile);
        string connectionString = $"Data Source={path}";
        optionsBuilder.UseSqlite(connectionString);
    }
}



