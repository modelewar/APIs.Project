using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.Core.Entites;
using Talabat.Core.Specifications;
using Talabate.Core.Entites;
using Talabate.Core.Repositories;

namespace Talabat.APIs.Controllers
{
    public class ProductsController : APIBaseController
    {
        private readonly IGenaricRepository<Product> _productRepo;

        public ProductsController(IGenaricRepository<Product> productRepo)
        {
            _productRepo = productRepo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var Spec = new ProductWitthBrandAndTypeSpecification();
            var Products = await _productRepo.GetAllWithSpecAsync(Spec);
            
            return Ok(Products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var Spec = new ProductWitthBrandAndTypeSpecification(id);
            var Product = await _productRepo.GetByIdWithSpecAsync(Spec);

            return Ok(Product);
        }
    }
}
