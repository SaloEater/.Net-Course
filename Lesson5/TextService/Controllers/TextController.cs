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
using Core.Contract;
using System.Text.Json;

namespace TextService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class TextController : ControllerBase, ITextController
    {
        private ITextRepository TextRepository;

        public TextController(ITextRepository textRepository)
        {
            TextRepository = textRepository;
        }

        [HttpPost("file")]
        public int file([FromBody] IFormFile file)
        {
            var text = new Text();
            using (var ms = new MemoryStream()) {
                file.CopyTo(ms);
                text.Content = System.Text.Encoding.Default.GetString(ms.ToArray());
            }
            TextRepository.Save(text);
            return text.Id.GetValueOrDefault();
        }

        [HttpPost("raw")]
        public int raw([FromForm] string raw)
        {
            var text = new Text() {
                Content = raw
            };
            TextRepository.Save(text);
            return text.Id.GetValueOrDefault();
        }

        [HttpPost("uri")]
        public int uri([FromQuery]string uri)
        {
            var text = new Text();
            using (var client = new WebClient()) {
                text.Content = client.DownloadString(uri);
            }
            TextRepository.Save(text);
            return text.Id.GetValueOrDefault();
        }

        [HttpGet("getById")]
        public string getById([FromQuery] int id)
        {
            return TextRepository.FindOneById(id).Content;
        }

        [HttpGet("getByIds")]
        public string getByIds([FromQuery] int[] ids)
        {
            var texts = TextRepository.FindByIds(ids).Select(i => new Core.ResponseDto.Text() { Id = i.Id, Content = i.Content }).ToArray();
            return JsonSerializer.Serialize(texts);
        }

        [HttpGet("getIds")]
        public string getIds()
        {
            return JsonSerializer.Serialize<int[]>(TextRepository.GetIds());
        }
    }
}
