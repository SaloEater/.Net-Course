using FindClient.Entity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskClient.Entity
{
    public class Task
    {
        public Guid TextId;

        public SingleFind[] Finds;
    }
}
