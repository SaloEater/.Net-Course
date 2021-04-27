using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskService.Entity;

namespace TaskService.Contract
{
    public interface ITaskRepository
    {
        public void Save(TaskEntity task);

        public TaskEntity GetById(int id);
    }
}
