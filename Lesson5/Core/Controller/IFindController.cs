using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Controller
{
    public interface IFindController
    {
        public void file(IFormFile file);

        public void raw(string raw);

        public void uri(string uri);

        public string getById(int id);

        public Core.ResponseDto.Text[] getByIds(int[] ids);

        public int[] getIds();
    }
}
