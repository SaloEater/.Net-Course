using RepositoryBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseEntity
{
    public class Task : EntityBase
    {
        public List<TaskText> TasksTexts;

        public List<Word> Words;

        public DateTime DateStart { get; set; }

        public DateTime DateEnd { get; set; }

        public string CancellationToken { get; set; }

        public int Interval { get; set; }
    }
}
