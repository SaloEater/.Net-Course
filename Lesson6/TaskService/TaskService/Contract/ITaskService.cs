using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace TaskService.Contract
{
    interface ITaskService
    {
        public Task<Guid> Start(string dateStart, string dateEnd, int interval, string[] words);

        public Task<TaskClient.Entity.Task[]> Info(Guid id);
    }
}
