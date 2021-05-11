using RepositoryBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseEntity
{
    [Table("task")]
    public class Task : EntityBase
    {
        public ICollection<TaskText> TasksTexts;

        public ICollection<Word> Words;

        public DateTime dateStart;

        public DateTime dateEnd;

        public string CancellationToken;

        public int interval;
    }
}
