using Ecom.Core.Interfaces;
using Ecom.infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.infrastructure.Repositories
{ 
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        private readonly AppDbContext _context;
        public GenericRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(T entity)
        {
            await _context.Set<T>().AddAsync(entity);
            await _context.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity= await _context.Set<T>().FindAsync(id);
            _context.Set<T>().Remove(entity);
            
            await _context.SaveChangesAsync();
        }

        public async Task<IReadOnlyList<T>> GetAllAsynnc()=> await _context.Set<T>().AsNoTracking().ToListAsync();
        public async Task<IReadOnlyList<T>> GetAllAsynnc(params Expression<Func<T, object>>[] includes)
        {
            var quary = _context.Set<T>().AsQueryable();
            foreach (var item in includes)
            {
                quary = quary.Include(item);
                
            }
            return await quary.ToListAsync();
        }

        public async Task<T> GetByIdAsync(int id, params Expression<Func<T, object>>[] includes)
        {
            var quary = _context.Set<T>().AsQueryable();
            foreach (var item in includes)
            {
                quary = quary.Include(item);

            }
           var entity = await quary.FirstOrDefaultAsync(x=>EF.Property<int>(x,"Id")==id);
            return entity;

        }

        public async Task<T> GetByIdAsync(int id)
        {
            var entity = await _context.Set<T>().FindAsync(id);
            return entity;

        }

        public async Task UpdateAsync(T entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            await _context.SaveChangesAsync();

        }
    }
}
