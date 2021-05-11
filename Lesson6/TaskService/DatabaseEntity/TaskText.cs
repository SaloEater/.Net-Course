using RepositoryBase;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseEntity
{
    [Table("tasks_texts")]
    public class TaskText : EntityBase
    {
        public Guid TextId;

        public int Count;

        public Guid TaskId;

        public virtual Task Task { get; set; }

        public Guid WordId;

        public virtual Word Word { get; set; }
    }
}
