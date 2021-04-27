using Core.Contract;
using Core.ResponseDto;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace FindService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FindController : ControllerBase, IFindController
    {
        private ITextController TextController;

        public FindController(ITextController textController)
        {
            TextController = textController;
        }

        [HttpGet("find")]
        public string find([FromQuery]int id, [FromQuery] string[] words)
        {
            var text = TextController.getById(id);
            var response = new Find();
            foreach (string word in words) {
                int matches = text.Split(word).Length - 1;
                response.wordsMatches.Add(word, matches);
            }
            var r = JsonSerializer.Serialize(response, new JsonSerializerOptions() { IncludeFields = true });
            return r;
        }
    }
}
