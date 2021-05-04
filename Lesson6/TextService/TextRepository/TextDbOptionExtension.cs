﻿using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TextRepository
{
    public static class TextDbOptionExtension
    {
        public static void AddTextDbOption(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<TextDbOption>(options =>
                options.ConnectionString = configuration.GetConnectionString("Text"));
        }
    }
}
