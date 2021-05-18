using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AuthentificationBase.Configuration;

namespace OwnAuthentificationBase.Configuration
{
    public static class OwnAuthenticationExtension
    {
        public static IServiceCollection AddOwnAppAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            return services.AddAppAuthentication(configuration);
        }
    }
}
