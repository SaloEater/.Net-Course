using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TextService.Contract;
using TextService.Entity;

namespace TextService.Repository
{
    public class ArrayTextRepository : ITextRepository
    {
        private Dictionary<int, Text> Texts = new();
        private int NextId = 1;

        public Text[] FindAll()
        {
            return Texts.Values.ToArray();
        }

        public Text[] FindByIds(int[] ids)
        {
            bool v = ids.Contains(25);
            return Texts.Where(i => ids.Contains(i.Key)).Select(i => i.Value).ToArray();
        }

        public Text FindOneById(int id)
        {
            return Texts[id];
        }

        public int[] GetIds()
        {
            return Texts.Keys.ToArray();
        }

        public void Save(Text text)
        {
            if (!text.Id.HasValue) {
                text.Id = NextId++;
            }

            Texts.Add(text.Id.Value, text);
        }
    }
}
