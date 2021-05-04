using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TextClient.Entity;

namespace TextService.Contract
{
    public interface ITextService
    {
        Task<TextFile> GetById(Guid id);
        Task<TextFile[]> GetSomeByIds(Guid[] ids);
        Task<Guid[]> GetIds();
        Task<TextFile> AddFile(string text);
    }
}
