using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskService.Contract;
using TaskService.Entity;

namespace TaskService.Repository
{
    public class ArrayTaskRepository : ITaskRepository
    {
        private Dictionary<int, TaskEntity> tasks = new();
        private int NextId = 1;

        public TaskEntity GetById(int id)
        {
            return tasks[id];
        }

        public void Save(TaskEntity task)
        {
            if (!task.Id.HasValue) {
                task.Id = NextId++;
            }

            tasks.Add(task.Id.Value, task);
        }
    }
}
