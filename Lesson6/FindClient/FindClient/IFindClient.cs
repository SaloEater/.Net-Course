using FindClient.Entity;
using System;
using System.Threading.Tasks;
using Refit;

namespace FindClient
{
    public interface IFindClient
    {
        [Get("/find/{id}")]
        Task<SingleFind[]> Find([Query] Guid id, [Query(CollectionFormat.Multi)] string[] words);
    }
}
