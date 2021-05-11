using RepositoryBase;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DatabaseEntity
{
    [Table("word")]
    public class Word : EntityBase
    {
        public string Content;

        public virtual ICollection<TaskText> TasksTexts { get; set; }
    }
}
