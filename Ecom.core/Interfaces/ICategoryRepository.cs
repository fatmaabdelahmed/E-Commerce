using Ecom.Core.Entities.Product;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace Ecom.Core.Interfaces
{
    public interface ICategoryRepository : IGenericRepository<Category>
    {
        //public Task AddAsync(Category entity)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task DeleteAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IReadOnlyList<Category>> GetAllAsynnc()
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<IReadOnlyList<Category>> GetAllAsynnc(params Expression<Func<Category, object>>[] includes)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Category> GetByIdAsync(int id, params Expression<Func<Category, object>>[] includes)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task<Category> GetByIdAsync(int id)
        //{
        //    throw new NotImplementedException();
        //}

        //public Task UpdateAsync(Category entity)
        //{
        //    throw new NotImplementedException();
        //}
    }
}
 