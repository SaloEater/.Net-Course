using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TaskClient;

namespace TaskService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase, ITaskClient
    {
        [HttpGet("info/{id}")]
        public Task<TaskClient.Entity.Task[]> Info([FromQuery] Guid id)
        {
            throw new NotImplementedException();
        }

        [HttpPost("start")]
        public Guid Start([FromForm] string dateStart, [FromForm] string dateEnd, [FromForm] int interval, [FromForm] string[] words)
        {
            throw new NotImplementedException();
        }
    }
}
