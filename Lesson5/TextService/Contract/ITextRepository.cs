using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using TextService.Entity;

namespace TextService.Contract
{
    public interface ITextRepository
    {
        public void Save(Text text);
        public Text FindOneById(int id);
        public Text[] FindAll();
        public Text[] FindByIds(int[] ids);
        public int[] GetIds();
    }
}
