using RepositoryBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEntity
{
    public class Word : EntityBase
    {
        public string Text { get; set; }

        public Guid TaskId { get; set; }

        public virtual Task Task { get; set; }

        public virtual List<TaskText> TasksTexts { get; set; }
    }
}
