using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Talabat.APIS.DTOs;
using Talabat.APIS.Errors;
using Talabat.Core.Entites;
using Talabat.Core.Specifications;
using Talabate.Core.Entites;
using Talabate.Core.Repositories;

namespace Talabat.APIs.Controllers
{
    public class ProductsController : APIBaseController
    {
        private readonly IGenaricRepository<ProductBrand> _brandRepo;
        private readonly IGenaricRepository<ProductType> _typeRepo;
        private readonly IMapper _mapper;
        private readonly IGenaricRepository<Product> _productRepo;

        public ProductsController(IGenaricRepository<Product> productRepo ,IMapper mapper
            , IGenaricRepository<ProductType> ProductType, IGenaricRepository<ProductBrand> ProductBrand)
        {
            _brandRepo = ProductBrand;
            _typeRepo = ProductType;
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
        [ProducesResponseType(typeof(ProductToReturnDto),200)]
        [ProducesResponseType(typeof(ApiResponse), StatusCodes.Status404NotFound)]

        public async Task<ActionResult<Product>> GetProduct(int id)
        {
            var Spec = new ProductWitthBrandAndTypeSpecification(id);
            var Product = await _productRepo.GetByIdWithSpecAsync(Spec);
            if (Product == null) return NotFound(new ApiResponse(404));
            var MappedProduct = _mapper.Map<Product , ProductToReturnDto>(Product);

            return Ok(MappedProduct);
        }

        //Get All Product Type 
        //URl BaseURl/api/Products/Types
        [HttpGet("Types")]
        public async Task<ActionResult<IEnumerable<ProductType>>> GetTypes()
        {
            var Types  = await _typeRepo.GetAllAsync();
            return Ok(Types);
        }



        //Gett ProductBrand 
        [HttpGet("Brands")]
        public async Task<ActionResult<IEnumerable<ProductBrand>>> GetProductBrand()
        {
            var Brands = await _brandRepo.GetAllAsync();
            return Ok(Brands);
        }




    }
}
