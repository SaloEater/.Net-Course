
using Core.ResponseDto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Contract
{
    public interface IFindController
    {
        public string find(int id, string[] words);
    }
}
