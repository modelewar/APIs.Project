using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabat.Core.Specifications;
using Talabate.Core.Entites;

namespace Talabate.Core.Repositories
{
    public interface IGenaricRepository<T> where T : BaseEntity
    {
        #region Without Specifications
        Task<IEnumerable<T>> GetAllAsync();
        Task<T> GetByIdAsync(int id); 
        #endregion

        Task<IEnumerable<T>> GetAllWithSpecAsync(ISpecifications<T> Spec);
        Task<T> GetByIdWithSpecAsync(ISpecifications<T> Spec);

    }
}
