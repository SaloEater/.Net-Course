using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RepositoryBase
{
    public interface IRepositoryBase<T>
    {
        abstract Task<T> GetById(Guid id);
        abstract Task<IEnumerable<T>> GetAll();
        abstract Task<T> Create(T entity);
        abstract Task<IEnumerable<T>> CreateMany(IEnumerable<T> entities);
        abstract Task<bool> Update(T entity);
        abstract Task<bool> UpdateMany(IEnumerable<T> entities);
        abstract Task<bool> Delete(Guid id);
        abstract Task<bool> DeleteMany(IEnumerable<Guid> id);
        abstract Task<bool> Restore(Guid id);
        abstract Task<bool> RestoreMany(IEnumerable<Guid> id);
    }
}
