using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contract
{
    public interface ITaskController
    {
        public int start(string dateStart, string dateFinish, int interval, string[] words);

        public string info(int id);
    }
}
