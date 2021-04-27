using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;

namespace Core.Contract
{
    public interface ITextController
    {
        public int file(IFormFile file);

        public int raw(string raw);

        public int uri(string uri);

        public string getById(int id);

        public string getByIds(int[] ids);

        public string getIds();
    }
}
