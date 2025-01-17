using System;
using System.IO;
using GhostBot.EntityModels;
using Microsoft.EntityFrameworkCore;

namespace GhostBot.DataContext {
    public partial class GhostBotContext : DbContext 
    {
        public GhostBotContext () { }

        public GhostBotContext(DbContextOptions<GhostBotContext> options) : base (options) { }

        public virtual DbSet<Person>? Person { get; set; }
        public virtual DbSet<Comment>? Comment { get; set; }
        public virtual DbSet<Category>? Category { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) 
        {
            if (!optionsBuilder.IsConfigured)
            {
            string database = "GhostBot.db";
            string dir = Environment.CurrentDirectory;
            string path = string.Empty;

            if (dir.EndsWith("net8.0"))
            {
                // In the <project>\bin\<Debug|Release>\net8.0 directory.
                path = Path.Combine("..", "..", "..", "..", database);
            }
            else
            {
                // In the <project> directory.
                path = Path.Combine("..", database);
            }

            path = Path.GetFullPath(path); // Convert to absolute path.

            try
            {
                GhostBotContextLogger.WriteLine($"Database path: {path}");
            }
            catch (Exception ex)
            {
                WriteLine(ex.Message);
            }

            if (!File.Exists(path))
            {
                throw new FileNotFoundException(
                message: $"{path} not found.", fileName: path);
            }

            optionsBuilder.UseSqlite($"Data Source={path}");

            optionsBuilder.LogTo(GhostBotContextLogger.WriteLine,
                new[] { Microsoft.EntityFrameworkCore
                .Diagnostics.RelationalEventId.CommandExecuting });
            }
        }
    }
}