using System;
using System.IO;
using Microsoft.EntityFrameworkCore;

namespace GhostBot.EntityModels {
    public class GhostBotDb : DbContext {
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            string dataBaseFile = "GhostBot.Db";
            string path = Path.Combine(Environment.CurrentDirectory, dataBaseFile);

            string connectionString = $"Data Source={path}";
            WriteLine($"Connection: {connectionString}");
            optionsBuilder.UseSqlite(connectionString);
        }
    }
}