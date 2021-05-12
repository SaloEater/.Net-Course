using Contract;
using DatabaseEntity;
using FindClient;
using FindClient.Entity;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using TaskClient.Entity;
using TaskService.Contract;
using TextClient;

namespace TaskService.Service
{
    public class TaskService : ITaskService
    {
        private readonly ITaskRepository TaskRepository;
        private readonly ITextClient TextClient;
        private readonly IFindClient FindClient;

        private const string DATE_FORMAT = "dd/MM/yyyy HH:mm:ss";

        private static List<BackgroundWorker> Workers;

        public TaskService(
            ITaskRepository taskRepository,
            ITextClient textClient,
            IFindClient findClient)
        {
            TaskRepository = taskRepository;
            TextClient = textClient;
            FindClient = findClient;
            Workers = new();
        }

        public async Task<TaskClient.Entity.Task[]> Info(Guid id)
        {
            var task = await TaskRepository.GetById(id);
            var responseTasks = new List<TaskClient.Entity.Task>();

            foreach (var textId in task.TasksTexts.Select(i => i.TextId).Distinct()) {
                var responseTask = new TaskClient.Entity.Task();
                responseTask.TextId = textId;
                var finds = new List<SingleFind>();
                foreach (var taskText in task.TasksTexts.Where(i => i.TextId == textId)) {
                    var singleFind = new SingleFind() {
                        Matched = taskText.Count,
                        Word = task.Words.First(i => i.Id == taskText.WordId).Text
                    };
                    finds.Add(singleFind);
                }
                responseTask.Finds = finds.ToArray();
                responseTasks.Add(responseTask);
            }

            return responseTasks.ToArray();
        }
        public async Task<Guid> Start(string dateStart, string dateEnd, int interval, string[] words)
        {
            CultureInfo provider = CultureInfo.InvariantCulture;
            var task = new DatabaseEntity.Task()
            {
                DateStart = DateTime.ParseExact(dateStart, DATE_FORMAT, provider),
                DateEnd = DateTime.ParseExact(dateEnd, DATE_FORMAT, provider),
                Interval = interval,
                Words = new(),
                TasksTexts = new()
            };

            var entities = new List<Word>();
            foreach (var word in words) {
                var entity = new Word { Text = word};
                task.Words.Add(entity);
            }

            CancellationTokenSource cancelTokenSource = new CancellationTokenSource();
            CancellationToken token = cancelTokenSource.Token;
            //task.CancellationToken = token.ToString();
            await TaskRepository.Create(task);

            var t = await TaskRepository.GetById(task.Id);
            var worker = new BackgroundWorker();
            worker.DoWork += async delegate (object sender, DoWorkEventArgs args) {
                do {
                    var taskId = task.Id;
                    if (token.IsCancellationRequested || DateTime.Now > task.DateEnd) {
                        break;
                    }

                    if (DateTime.Now > task.DateStart) {
                        var existingTexts = await TextClient.GetIds();
                        var task = await TaskRepository.GetById(taskId);
                        var processedTexts = task.TasksTexts?.Select(i => i.TextId).ToArray() ?? Array.Empty<Guid>();
                        var idDiff = existingTexts.Except(processedTexts);
                        if (idDiff.Any()) {
                            foreach (var textId in idDiff) {
                                var response = await FindClient.Find(textId, task.Words.Select(i => i.Text).ToArray());
                                foreach (var find in response) {
                                    var taskText = new TaskText()
                                    {
                                        Count = find.Matched,
                                        WordId = task.Words.First(i => i.Text == find.Word).Id,
                                        TaskId = task.Id,
                                        TextId = textId
                                    };
                                    task.TasksTexts.Add(taskText);
                                }
                            }
                            await TaskRepository.Update(task);
                        }
                    }

                    Thread.Sleep(interval * 60 * 1000);
                } while (true);
            };
            Workers.Add(worker);
            worker.RunWorkerAsync();
            return task.Id;
        }
    }
}
