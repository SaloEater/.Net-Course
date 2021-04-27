using Core.Contract;
using Core.ResponseDto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text.Json;
using System.Threading;
using TaskService.Contract;
using TaskService.Entity;

namespace TaskService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TaskController : ControllerBase, ITaskController
    {
        private const string DATE_FORMAT = "dd/MM/yyyy HH:mm:ss";
        private IFindController FindController;
        private ITaskRepository TaskRepository;
        private ITextController TextController;

        static List<BackgroundWorker> Workers = new();

        public TaskController(
            IFindController findController, 
            ITaskRepository taskRepository,
            ITextController textController
        ) {
            FindController = findController;
            TaskRepository = taskRepository;
            TextController = textController;
        }

        [HttpGet("info")]
        public string info([FromQuery]int id)
        {
            var task = TaskRepository.GetById(id);
            return JsonSerializer.Serialize(task.matches, new JsonSerializerOptions() { IncludeFields = true });
        }

        [HttpPost("start")]
        public int start([FromForm] string dateStart, [FromForm] string dateEnd, [FromForm] int interval, [FromForm] string[] words)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            var task = new TaskEntity() {
                dateStart = DateTime.ParseExact(dateStart, DATE_FORMAT, provider),
                dateEnd = DateTime.ParseExact(dateEnd, DATE_FORMAT, provider),
                interval = interval,
                words = words
            };

            TaskRepository.Save(task);

            var worker = new BackgroundWorker();
            worker.DoWork += delegate (object sender, DoWorkEventArgs args) {
                do {
                    if (DateTime.Now > task.dateEnd) {
                        break;
                    }

                    if (DateTime.Now > task.dateStart) {
                        var existingTexts = JsonSerializer.Deserialize<int[]>(TextController.getIds());
                        var processedTexts = task.matches.Select(k => k.Key).ToArray();
                        var diff = existingTexts.Except(processedTexts);
                        if (diff.Any()) {
                            foreach (var textId in diff) {
                                var response = FindController.find(textId, task.words);
                                Find dto = JsonSerializer.Deserialize<Find>(response, new JsonSerializerOptions() { IncludeFields = true });
                                task.matches.Add(textId, dto);
                            }
                        }
                    }                    

                    Thread.Sleep(interval * 60 * 1000);
                } while (true);
            };
            Workers.Add(worker);
            worker.RunWorkerAsync();
            return task.Id.Value;
        }
    }
}
