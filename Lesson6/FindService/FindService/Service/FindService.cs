using FindClient.Entity;
using FindService.Contract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TextClient;

namespace FindService.Service
{
    public class FindService : IFindService
    {
        private readonly ITextClient TextClient;

        public FindService(ITextClient textClient)
        {
            TextClient = textClient;
        }

        public async Task<SingleFind[]> Find(Guid id, string[] words)
        {
            var text = await TextClient.GetById(id);
            var result = new List<SingleFind>();
            foreach (string word in words)
            {
                int matches = text.TextValue.Split(word).Length - 1;
                var item = new SingleFind() { Word = word, Matched = matches };
                result.Add(item);
            }
            return result.ToArray();
        }
    }
}
