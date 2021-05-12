using RepositoryBase;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseEntity
{
    public class TaskText : EntityBase
    {
        public Guid TextId { get; set; }

        public int Count { get; set; }

        public Guid TaskId { get; set; }

        public virtual Task Task { get; set; }

        public Guid WordId { get; set; }

        public virtual Word Word { get; set; }
    }
}
