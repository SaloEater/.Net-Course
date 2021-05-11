using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using Refit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace TaskClient.Configuration
{
    public static class TaskClientExtension
    {
        public static void AddTaskClient(this IServiceCollection services, IConfiguration configuration)
        {
            services.TryAddTransient(_ => RestService.For<ITaskClient>(new HttpClient()
            {
                BaseAddress = new Uri(configuration["ServiceUrls:TaskService"])
            }));
        }
    }
}
