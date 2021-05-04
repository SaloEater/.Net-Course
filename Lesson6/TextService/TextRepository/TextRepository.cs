using DatabaseEntity;
using System;
using EFRepositoryBase;
using Microsoft.Extensions.Options;
using Contract;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace TextRepository
{
    public class TextRepository : EFContextBase<Text>, ITextRepository
    {
        public TextRepository(IOptions<TextDbOption> textDbOptions) : base(textDbOptions)
        {

        }

        public async Task<Text[]> GetByIds(Guid[] ids)
        {
            List<Text> entities = new();
            foreach (var id in ids) {
                var entity = await Entities.FindAsync(id);
                entities.Add(entity);
            }
            return entities.ToArray();
        }
    }
}
