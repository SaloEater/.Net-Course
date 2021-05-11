using FindClient.Entity;
using Refit;
using System;
using System.Threading.Tasks;

namespace TaskClient
{
    public interface ITaskClient
    {
        [Get("task/Info/{id}")]
        public Task<Entity.Task[]> Info([Query] Guid id);

        [Post("task/Start")]
        public Guid Start([Body] string dateStart, [Body] string dateEnd, [Body] int interval, [Body] string[] words);
    }
}
