using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskClient;
using TaskService.Contract;

namespace TaskService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase, ITaskClient
    {
        private readonly ITaskService TaskService;

        public TaskController(ITaskService taskService)
        {
            TaskService = taskService;
        }

        [HttpGet("info")]
        public async Task<TaskClient.Entity.Task[]> Info([FromQuery] Guid id)
        {
            return await TaskService.Info(id);
        }

        [HttpPost("start")]
        public async Task<Guid> Start([FromForm] string dateStart, [FromForm] string dateEnd, [FromForm] int interval, [FromForm] string[] words)
        {
            return await TaskService.Start(dateStart, dateEnd, interval, words);
        }
    }
}
