using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Talabate.Core.Entites;

namespace Talabat.Core.Specifications
{
    public interface ISpecifications<T> where T : BaseEntity
    {
        //_dbContext.Products..where(p=>p.id==id)Include(p => p.ProductBrand).Include(p => p.ProductType);

        //Sign For Property For Where Condition [where(p=>p.id==id)]
        public Expression<Func<T , bool>> Criteria { get; set; }
        // Sign For Prperty For List O f Includes [Include(p => p.ProductBrand).Include(p => p.ProductType)]
        public List<Expression<Func<T, object>>> Includes { get; set; }
    }
}
