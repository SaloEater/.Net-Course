using Contract;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskEFRepository
{
    public static class TaskRepositoryExtension
    {
        public static void AddTaskRepository(this IServiceCollection services)
        {
            services.AddSingleton<ITaskRepository, TaskRepository>();
        }
    }
}
