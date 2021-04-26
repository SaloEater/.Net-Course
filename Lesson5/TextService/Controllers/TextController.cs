using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TextService.Contract;
using TextService.Entity;
using System.Web;
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Net;
using Core.Controller;

namespace TextService.Controllers
{
    [ApiController]
    public class TextController : ControllerBase, ITextController
    {
        private ITextRepository TextRepository;

        public TextController(ITextRepository textRepository)
        {
            TextRepository = textRepository;
        }

        [HttpPost]
        public void file(IFormFile file)
        {
            var text = new Text();
            using (var ms = new MemoryStream()) {
                file.CopyTo(ms);
                text.Content = System.Text.Encoding.Default.GetString(ms.ToArray());
            }
            TextRepository.Save(text);
        }

        [HttpPost]
        public void raw(string raw)
        {
            var text = new Text() {
                Content = raw
            };
            TextRepository.Save(text);
        }

        [HttpPost]
        public void uri(string uri)
        {
            var text = new Text();
            using (var client = new WebClient()) {
                text.Content = client.DownloadString(uri);
            }
            TextRepository.Save(text);
        }

        [HttpGet]
        public string getById(int id)
        {
            return TextRepository.FindOneById(id).Content;
        }

        [HttpGet]
        public Core.ResponseDto.Text[] getByIds(int[] ids)
        {
            return TextRepository.FindByIds(ids).Select(i => new Core.ResponseDto.Text() { Id = i.Id, Content = i.Content}).ToArray();
        }

        [HttpGet]
        public int[] getIds()
        {
            return TextRepository.GetIds();
        }
    }
}
