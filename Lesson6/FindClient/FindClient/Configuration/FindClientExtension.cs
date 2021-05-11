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
        public static void AddFindServiceClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddTransient(_ => RestService.For<IFindClient>(new HttpClient()
            {
                BaseAddress = new Uri(configuration["ServiceUrls:FindService"])
            }));
        }
    }
}
