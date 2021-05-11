using FindClient.Entity;
using System;
using System.Threading.Tasks;

namespace FindService.Contract
{
    public interface IFindService
    {
        public Task<SingleFind[]> Find(Guid id, string[] words);
    }
}
