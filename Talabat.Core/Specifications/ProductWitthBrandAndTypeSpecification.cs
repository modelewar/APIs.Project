using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Talabate.Core.Entites;

namespace Talabat.Core.Specifications
{
    public class ProductWitthBrandAndTypeSpecification: BaseSpecification<Product>
    {
        public ProductWitthBrandAndTypeSpecification():base()
        {
            Includes.Add( p=>p.ProductBrand);
            Includes.Add(p => p.ProductType);
        }
    }
}
