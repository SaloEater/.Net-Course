using Refit;
using System;
using System.IO;
using System.Threading.Tasks;
using TextClient.Entity;

namespace TextClient
{
    public interface ITextClient
    {
        [Get("/text/{id}")]
        Task<TextFile> GetById(Guid id);

        [Get("/text/{ids}")]
        Task<TextFile[]> GetByIds(Guid[] ids);

        [Get("/text/ids")]
        Task<Guid[]> GetIds();

        [Post("/text")]
        Task<TextFile> Post([Body] string text);

        [Post("/text/file/{streamTextFile}")]
        Task<TextFile> PostFile(Stream streamTextFile);

        [Post("/text/url/{fileUrl}")]
        Task<TextFile> PostFileUrl([Body] string fileUrl);
    }
}
