using Core.ResponseDto;
using System;
using System.Collections.Generic;

namespace TaskService.Entity
{
    public class TaskEntity
    {
        public int? Id;
        public DateTime dateStart;
        public DateTime dateEnd;
        public int interval;
        public string[] words;
        public Dictionary<int, Find> matches = new();
    }
}
