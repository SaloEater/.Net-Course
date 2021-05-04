using Contract;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace TextRepository
{
    public static class TextRepositoryExtension
    {
        public static void AddTextRepository(this IServiceCollection services)
        {
            services.AddTransient<ITextRepository, TextRepository>();
        }
    }
}
