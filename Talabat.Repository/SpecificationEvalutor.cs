using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Specifications;
using Talabate.Core.Entites;

namespace Talabat.Repository
{
    public class SpecificationEvalutor<T> where T : BaseEntity
    {
        //Fun To Build Query 
        // _dbContext.Set<T>.Where(p => p.Id == id).Include(p => p.ProductBrand).Include(p => p.ProductType);

        public static IQueryable<T> GetQuery(IQueryable<T> inputQuery , ISpecifications<T> Spec)
        {
            var Query = inputQuery; //_dbContext.Set<T>
            if (Spec.Criteria is not null)
            {
                Query = Query.Where(Spec.Criteria); //_dbContext.Set<T>.Where(p => p.Id == id)

            }
            //Include(p => p.ProductBrand).Include(p => p.ProductType);
            Query = Spec.Includes.Aggregate(Query , (CurrentQuery,includeExpression)=>CurrentQuery.Include(includeExpression));
            //_dbContext.Set<T>.Where(p => p.Id == id).Include(p => p.ProductBrand)
            //_dbContext.Set<T>.Where(p => p.Id == id).Include(p => p.ProductBrand).Include(p => p.ProductType);
            return Query;
        }
    }
}
