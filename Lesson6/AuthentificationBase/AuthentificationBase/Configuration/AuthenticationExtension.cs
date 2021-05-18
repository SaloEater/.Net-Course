using AuthentificationBase.Model;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System;

namespace AuthentificationBase.Configuration
{
    public static class AuthenticationExtension
    {
        private const string CURRENT_ASSEMBLY = "AuthentificationBase";

        public static IServiceCollection AddAppAuthentication(this IServiceCollection services, IConfiguration configuration)
        {
            services.Configure<AuthOptions>(configuration.GetSection(AuthOptions.Security));

            services.AddDbContext<AuthenticationDbContext>(options => {
                options.UseSqlServer(configuration.GetConnectionString("Auth"), b => b.MigrationsAssembly(configuration.GetSection("MigrationAssembly").Value ?? CURRENT_ASSEMBLY));
            });

            services
                .AddIdentity<User, IdentityRole>()
                .AddEntityFrameworkStores<AuthenticationDbContext>()
                .AddDefaultTokenProviders();

            var authOptions = new AuthOptions();
            configuration.GetSection(AuthOptions.Security).Bind(authOptions);

            services.AddAuthentication(options =>
            {
                options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
                options.DefaultScheme = JwtBearerDefaults.AuthenticationScheme;
            }).AddJwtBearer(options =>
            {
                options.SaveToken = true;
                options.RequireHttpsMetadata = false;
                options.TokenValidationParameters = new TokenValidationParameters
                {
                    ValidateIssuer = true,
                    ValidIssuer = authOptions.Issuer,
                    ValidateAudience = true,
                    ValidAudience = authOptions.Audience,
                    ValidateLifetime = true,

                    IssuerSigningKey = authOptions.GetSymmetricSecurityKey(),
                };
            });

            return services;
        }
    }
}