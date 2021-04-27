using Core.Contract;
using Core.ResponseDto;
using Core.ValueObject;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Controller
{
    public class TextController : ITextController
    {
        private string Uri;
        private const string name = "text";

        public TextController()
        {
            Uri = new MicroserviceRouteConfiguration(name).uri;
        }

        public string getById(int id)
        {
            using (var client = new WebClient()) {
                return client.DownloadString($"{Uri}/{name}/getById?id={id}");
            }
        }

        public string getByIds(int[] ids)
        {
            using (var client = new WebClient()) {
                string idsQuery = "";
                foreach (var id in ids) {
                    idsQuery += $"ids={id}&";
                }
                return client.DownloadString($"{Uri}/{name}/getByIds?{idsQuery}");
            }
        }

        public string getIds()
        {
            using (var client = new WebClient()) {
                return client.DownloadString($"{Uri}/{name}/getIds");
            }
        }

        public int file(IFormFile file)
        {
            throw new NotImplementedException();
        }

        public int raw(string raw)
        {
            throw new NotImplementedException();
        }

        public int uri(string uri)
        {
            throw new NotImplementedException();
        }
    }
}
