using System;
using System.Net.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Refit;

namespace AuthentificationBase.Configuration
{
    public static class HttpClientExtensions
    {
        public static IServiceCollection AddApiClient<T>(
            this IServiceCollection services,
            IConfiguration configuration,
            string configurationUrlKey,
            RefitSettings refitSettings = null
        ) where T : class {
            services.AddHttpClientHandlers();
            services.Configure<ApiSettings>(configuration.GetSection("Api"));

            var baseUrl = configuration[configurationUrlKey];

            if (string.IsNullOrWhiteSpace(baseUrl))
                throw new NullReferenceException($"BaseUrl is empty for {nameof(T)}");

            services.TryAddTransient<T>(provider =>
            {
                // HACK клиент собирается руками, т.к. factory не учитывает зарегистрированное время жизни для HttpMessageHandler
                // https://github.com/dotnet/runtime/issues/36574

                var httpMessageHandler = provider.BuildHandler();
                var client = new HttpClient(httpMessageHandler)
                {
                    BaseAddress = new Uri(baseUrl.TrimEnd('/'))
                };

                return RestService.For<T>(client, refitSettings ?? new RefitSettings());
            });

            return services;
        }

        internal static IServiceCollection AddHttpClientHandlers(this IServiceCollection services)
        {
            services.TryAddTransient<AuthenticatedHttpClientHandler>();

            return services;
        }

        private static HttpMessageHandler BuildHandler(this IServiceProvider provider)
        {
            var authHandler = provider.GetService<AuthenticatedHttpClientHandler>();

            return authHandler;
        }
    }
}