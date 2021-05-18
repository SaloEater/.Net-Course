using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using TextClient;
using TextClient.Entity;
using TextService.Contract;

namespace TextService.Controllers
{
    [Authorize]
    [ApiController]
    [Route("[controller]")]
    public class TextController : ControllerBase, ITextClient
    {
        private readonly ITextService TextService;

        public TextController(ITextService textService)
        {
            TextService = textService;
        }

        [HttpGet("one")]
        public async Task<TextFile> GetById([FromQuery] Guid id)
        {
            return await TextService.GetById(id);
        }

        [HttpGet("some")]
        public async Task<TextFile[]> GetByIds([FromQuery] Guid[] ids)
        {
            return await TextService.GetSomeByIds(ids);
        }

        [HttpGet("ids")]
        public async Task<Guid[]> GetIds()
        {
            return await TextService.GetIds();
        }

        [HttpPost("")]
        public async Task<TextFile> Post([FromForm] string text)
        {
            return await TextService.AddFile(text);
        }

        [HttpPost("post")]
        public async Task<TextFile> PostFile([FromForm] Stream streamTextFile)
        {
            var text = "";
            using (var ms = new MemoryStream()) {
                streamTextFile.CopyTo(ms);
                text = ms.ToString();
            }
            return await TextService.AddFile(text);
        }

        [HttpPost("url")]
        public async Task<TextFile> PostFileUrl([FromForm] string fileUrl)
        {
            var text = "";
            using (var client = new WebClient()) {
                text = client.DownloadString(fileUrl);
            }
            return await TextService.AddFile(text);
        }
    }
}
