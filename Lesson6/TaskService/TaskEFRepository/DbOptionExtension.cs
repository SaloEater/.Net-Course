using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using RepositoryBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEFRepository
{
    public static class DbOptionExtension
    {
        public static void AddDbOption(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<DbOption>(options =>
                options.ConnectionString = configuration.GetConnectionString("Task"));
        }
    }
}
