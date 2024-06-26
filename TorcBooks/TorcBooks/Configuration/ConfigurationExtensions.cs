﻿using TorcBooks.DAL;

namespace TorcBooks.Configuration
{
    public static class ConfigurationExtensions
    {
        public static void RegisterRepositories(this IServiceCollection services)
        {
            services.AddScoped<BooksRepository>();
        }
    }
}
