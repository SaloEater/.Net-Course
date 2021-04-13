using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Threading;

namespace Lesson4
{
    class Program
    {
        static Dictionary<string, string> FilesMap = new Dictionary<string, string>();
        static DateTime MapRefreshTime = DateTime.MinValue;

        static List<BackgroundWorker> Workers = new List<BackgroundWorker>();

        const string SOURCE_FOLDER = "E:\\Repos\\.Net-Course\\Lesson4\\Lesson4";
        const int REFRESH_INTERVAL = 1000 * 60 * 5;
        const int CACHED_DIRECTORY_DEPTH = 5;
        const int REFRESH_WAITER_INTERVAL = 250;

        static object PrintLock = new object();
        static object RefreshCacheLock = new object();

        volatile static int ReadersCount = 0;

        static void Main(string[] args)
        {
            CreateWorkers();
            Workers.ForEach(w => w.RunWorkerAsync());
            Console.Read();
        }

        private static void CreateWorkers()
        {
            CreateWorker("cs", 2400);
            CreateWorker("csproj", 5500);
            CreateWorker("json", 1250);
        }

        private static void CreateWorker(string fileFormat, int delay)
        {
            if (!fileFormat.StartsWith('.'))
            {
                fileFormat = '.' + fileFormat;
            }

            BackgroundWorker worker = new BackgroundWorker();
            worker.DoWork += delegate (object sender, DoWorkEventArgs args)
            {
                do
                {
                    Print(fileFormat);
                    Thread.Sleep(delay);
                } while (true);
            };
            Workers.Add(worker);
        }

        private static void Print(string fileFormat)
        {
            lock (RefreshCacheLock)
            {
                var checkTime = DateTime.Now;
                if ((checkTime - MapRefreshTime).TotalMilliseconds > REFRESH_INTERVAL)
                {
                    while (ReadersCount > 0)
                    {
                        Console.WriteLine($"{fileFormat}: Unable to refresh cache: {ReadersCount} workers are reading");
                        Thread.Sleep(REFRESH_WAITER_INTERVAL);
                    }
                    MapRefreshTime = checkTime;
                    UpdateFiles(SOURCE_FOLDER);
                }
            }
            ReadersCount++;
            var preparedData = FilesMap.Values.Where(i => Path.GetExtension(i).Equals(fileFormat)).ToList();
            ReadersCount--;
            lock (PrintLock)
            {
                Console.WriteLine($"*{fileFormat}:");
                preparedData.ForEach(i => Console.WriteLine(i));
            };
        }

        private static void UpdateFiles(string root)
        {
            FilesMap.Clear();
            UpdateFilesRecursive(root, 0);
            MapRefreshTime = DateTime.Now;
        }

        private static void UpdateFilesRecursive(string sourcePath, int level)
        {
            if (level > CACHED_DIRECTORY_DEPTH)
            {
                return;
            }
            foreach (var fileName in Directory.GetFiles(sourcePath))
            {
                FilesMap.Add(fileName, Path.GetFileName(fileName));
            }
            foreach (var childDir in Directory.GetDirectories(sourcePath))
            {
                UpdateFilesRecursive(childDir, level + 1);
            }
        }
    }
}
