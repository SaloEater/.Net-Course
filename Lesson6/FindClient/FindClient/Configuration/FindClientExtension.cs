using AuthentificationBase.Configuration;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Refit;
using System;
using System.Net.Http;

namespace FindClient.Configuration
{
    public static class FindClientExtension
    {
        private const string CONFIGURATION_URL_KEY = "ServiceUrls:FindService";

        public static void AddFindServiceClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddTransient(_ => RestService.For<IFindClient>(new HttpClient()
            {
                BaseAddress = new Uri(configuration[CONFIGURATION_URL_KEY])
            }));
        }

        public static IServiceCollection AddFindServiceWithAuthentificationClient(this IServiceCollection services,
            IConfiguration configuration)
        {
            return services.AddApiClient<IFindClient>(configuration, CONFIGURATION_URL_KEY);
        }
    }
}
