using DatabaseEntity;
using RepositoryBase;
using System;
using System.Threading.Tasks;

namespace Contract
{
    public interface ITextRepository : IRepositoryBase<Text>
    {
        Task<Text[]> GetByIds(Guid[] ids);
    }
}
