using GhostBot.EntityModels;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;

namespace GhostBot.DataContext {
    public static class GhostBotContextExtensionsExtensions {
        public static IServiceCollection AddGhostBotContext(this IServiceCollection services,
            string relativePath = "..", string databaseName = "GhostBot.db") {
            string path = Path.Combine (relativePath, databaseName);
            path = Path.GetFullPath (path);
            GhostBotContextLogger.WriteLine ($"Database path: {path}");

            if (!File.Exists (path)) {
                throw new FileNotFoundException (message: $"{path} not found.");
            }

            services.AddDbContext<GhostBotContext>(options => 
            {
                options.UseSqlite($"Data Source={path}");
                options.LogTo(GhostBotContextLogger.WriteLine, new [] {
                    Microsoft.EntityFrameworkCore.Diagnostics.RelationalEventId.CommandExecuting
                });
            },
            contextLifetime: ServiceLifetime.Transient,
            optionsLifetime: ServiceLifetime.Transient
            );

            return services;
        }
    }
}

