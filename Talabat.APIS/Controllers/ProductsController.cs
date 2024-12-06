using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.DTOs;
using Talabat.Core.Entites;
using Talabat.Core.Specifications;
using Talabate.Core.Entites;
using Talabate.Core.Repositories;

namespace Talabat.APIs.Controllers
{
    public class ProductsController : APIBaseController
    {
        private readonly IMapper _mapper;
        private readonly IGenaricRepository<Product> _productRepo;

        public ProductsController(IGenaricRepository<Product> productRepo ,IMapper mapper)
        {
            _mapper = mapper;
            _productRepo = productRepo;
        }
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Product>>> GetProducts()
        {
            var Spec = new ProductWitthBrandAndTypeSpecification();
            var Products = await _productRepo.GetAllWithSpecAsync(Spec);
            var MappedProducts = _mapper.Map<IEnumerable<Product>, IEnumerable<ProductToReturnDto>>(Products);
            return Ok(Products);
        }
        [HttpGet("{id}")]
        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var Spec = new ProductWitthBrandAndTypeSpecification(id);
            var Product = await _productRepo.GetByIdWithSpecAsync(Spec);
            var MappedProduct = _mapper.Map<Product , ProductToReturnDto>(Product);

            return Ok(Product);
        }
    }
}
