using Data_Access_Layer.Contracts;
using Data_Access_Layer.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Data_Access_Layer.Models
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class, new()
    {

        private readonly DataContext _context;

        public GenericRepository(DataContext context)
        {
            _context = context;
        }

        public async Task<T> Create(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<int> Delete(T entity)
        {
            var deletedEntry = _context.Set<T>().Remove(entity);
            return await _context.SaveChangesAsync();
        }

        public async Task<IEnumerable<T>> GetAll()
        {
            return await _context.Set<T>().ToListAsync();
        }

        public async Task<T> GetById(Guid id)
        {
            return await _context.Set<T>().FindAsync(id);
        }

        public Task<T> Find(Expression<Func<T, bool>> filter = null)
        {
            return _context.Set<T>().FirstOrDefaultAsync(filter);
        }

        public async Task<T> Update(T entity, Guid id)
        {
            if (entity == null)
                return null;
            T exist = await _context.Set<T>().FindAsync(id);
            if (exist != null)
            {
                _context.Entry(exist).CurrentValues.SetValues(entity);
                await _context.SaveChangesAsync();
            }
            return exist;
        }

        public async Task<IEnumerable<T>> FindAll(Expression<Func<T, bool>> filter = null)
        {
            return await _context.Set<T>().Where(filter).ToListAsync();
        }
    }
}
