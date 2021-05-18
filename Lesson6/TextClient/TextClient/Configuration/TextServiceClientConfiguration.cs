using AuthentificationBase.Configuration;
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
        private const string CONFIGURATION_URL_KEY = "ServiceUrls:TextService";

        public static IServiceCollection AddTextServiceClient(this IServiceCollection services,
            IConfiguration configuration)
        {
            services.TryAddTransient(_ => RestService.For<ITextClient>(new HttpClient() {
                BaseAddress = new Uri(configuration[CONFIGURATION_URL_KEY])
            }));

            return services;
        }

        public static IServiceCollection AddTextServiceWithAuthentificationClient(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.AddApiClient<ITextClient>(configuration, CONFIGURATION_URL_KEY);
        }
    }
}
