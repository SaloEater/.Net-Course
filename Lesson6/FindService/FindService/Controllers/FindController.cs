using FindClient;
using FindClient.Entity;
using FindService.Contract;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace FindService.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FindController : ControllerBase, IFindClient
    {
        private readonly IFindService FindService;

        public FindController(IFindService findService)
        {
            FindService = findService;
        }

        [HttpGet("")]
        public async Task<SingleFind[]> Find([FromQuery] Guid id, [FromQuery] string[] words)
        {
            return await FindService.Find(id, words);
        }
    }
}
