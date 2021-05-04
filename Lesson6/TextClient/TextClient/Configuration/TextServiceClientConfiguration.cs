using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Refit;
using System;
using System.Net.Http;

namespace TextClient.Configuration
{
    public static class TextServiceClientConfiguration
    {
        public static IServiceCollection AddTextServiceClient(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.TryAddTransient(_ => RestService.For<ITextClient>(new HttpClient() {
                BaseAddress = new Uri(configuration["ServiceUrls:TextService"])
            }));

            return services;
        }
    }
}
