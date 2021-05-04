using AutoMapper;
using Contract;
using DatabaseEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TextClient.Entity;
using TextService.Contract;

namespace TextService.Service
{
    public class TextService : ITextService
    {
        private readonly ITextRepository TextRepository;
        private readonly IMapper Mapper;

        public TextService(ITextRepository textRepository, IMapper mapper)
        {
            TextRepository = textRepository;
            Mapper = mapper;
        }

        public async Task<TextFile> AddFile(string text)
        {
            var source = new Text() { TextValue = text };
            await TextRepository.Create(source);
            return Mapper.Map<TextFile>(source);
        }

        public async Task<TextFile> GetById(Guid id)
        {
            var source = await TextRepository.GetById(id);
            return Mapper.Map<TextFile>(source);
        }

        public async Task<Guid[]> GetIds()
        {
            var source = await TextRepository.GetAll();
            return source.Select(i => i.Id).ToArray();
        }

        public async Task<TextFile[]> GetSomeByIds(Guid[] ids)
        {
            var source = await TextRepository.GetByIds(ids);
            List<TextFile> entities = new();
            foreach (var entity in source) {
                entities.Add(Mapper.Map<TextFile>(entity));
            }
            return entities.ToArray();
        }
    }
}
