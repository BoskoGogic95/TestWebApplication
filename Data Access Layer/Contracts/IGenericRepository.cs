using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Contracts
{
    public interface IGenericRepository<T> where T : class, new()
    {
        Task<IEnumerable<T>> GetAll();

        Task<T> GetById(Guid Id);

        Task<T> Create(T entity);

        Task<int> Delete(T entity);

        Task<T> Update(T entity, Guid id);

        Task<T> Find(Expression<Func<T, bool>> filter = null);

        Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> filter = null);
    }
}
