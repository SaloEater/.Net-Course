using RepositoryBase;
using System;
using System.ComponentModel.DataAnnotations.Schema;

namespace DatabaseEntity
{
    [Table("text")]
    public class Text : EntityBase
    {
        public string TextValue { get; set; }
    }
}
