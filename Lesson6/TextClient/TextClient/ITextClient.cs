using Refit;
using System;
using System.IO;
using System.Threading.Tasks;
using TextClient.Entity;

namespace TextClient
{
    public interface ITextClient
    {
        [Get("/text/one")]
        Task<TextFile> GetById(Guid id);

        [Get("/text/some")]
        Task<TextFile[]> GetByIds(Guid[] ids);

        [Get("/text/ids")]
        Task<Guid[]> GetIds();

        [Post("/text")]
        Task<TextFile> Post([Body] string text);

        [Post("/text/file")]
        Task<TextFile> PostFile(Stream streamTextFile);

        [Post("/text/url")]
        Task<TextFile> PostFileUrl([Body] string fileUrl);
    }
}
