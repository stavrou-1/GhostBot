using System;
using System.IO;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using GhostBot.EntityModels;

namespace GhostBot.DataContext;

public static class GhostBotContextExtensionsExtensions
{
    public static IServiceCollection AddGhostBotContext(this IServiceCollection services,
    string relativePath = "", string databaseName = "")
    {
        return services;
    }
}



