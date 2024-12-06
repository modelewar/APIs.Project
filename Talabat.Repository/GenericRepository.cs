using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Specifications;
using Talabat.Repository.Data;
using Talabate.Core.Entites;
using Talabate.Core.Repositories;

namespace Talabat.Repository
{
    public class GenericRepository<T> : IGenaricRepository<T> where T : BaseEntity
    {



        private readonly StoreContext _dbContext;

        public GenericRepository(StoreContext dbContext)
        {
            _dbContext = dbContext;
        }
        #region Without Specifications
        public async Task<IEnumerable<T>> GetAllAsync()
        {
            if (typeof(T)==typeof(Product))
                return (IEnumerable<T>)await _dbContext.Products.Include(p => p.ProductBrand).Include(p => p.ProductType).ToListAsync();

            else
                return await _dbContext.Set<T>().ToListAsync();
        }


        public async Task<T> GetByIdAsync(int id)
        {

            return await _dbContext.Set<T>().FindAsync(id);
            //if(typeof(T)==typeof(Product))
            //return _dbContext.Set<T>.Where(p => p.Id == id).Include(p => p.ProductBrand).Include(p => p.ProductType);
        } 
        #endregion


        #region With Specifications
        public async Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecifications<T> Spec)
        {
            return await ApplySpecifications(Spec).ToListAsync();
        }
        public async Task<T> GetByIdWithSpecAsync(ISpecifications<T> Spec)
        {
            return await ApplySpecifications(Spec).FirstOrDefaultAsync();
        } 
        #endregion

        private IQueryable<T> ApplySpecifications(ISpecifications<T> Spec)
        {
            return SpecificationEvalutor<T>.GetQuery(_dbContext.Set<T>(), Spec);
        }
    }
}
