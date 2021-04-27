using Core.Contract;
using Core.ValueObject;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Core.Controller
{
    public class FindController : IFindController
    {
        private string Uri;
        private const string name = "find";

        public FindController()
        {
            Uri = new MicroserviceRouteConfiguration(name).uri;
        }

        public string find(int id, string[] words)
        {
            using (var client = new WebClient()) {
                string idsQuery = "";
                foreach (var word in words) {
                    idsQuery += $"words={word}&";
                }
                return client.DownloadString($"{Uri}/{name}/find?id={id}&{idsQuery}");
            }
        }
    }
}
