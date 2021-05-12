using FindClient.Entity;
using System;

namespace TaskClient.Entity
{
    public class Task
    {
        public Guid TextId { get; set; }

        public SingleFind[] Finds { get; set; }
    }
}
