using API.Dtos;
using AutoMapper;
using Core.Entities;
using Core.Interfaces;
using Core.Specifications;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class ProductsController : ControllerBase
    {
        public readonly IGenericRepository<Product> _productsRepository;
        public readonly IGenericRepository<ProductBrand> _productBrandsRepository;
        public readonly IGenericRepository<ProductType> _productTypesRepository;
        private readonly IMapper _mapper;

        public ProductsController(IGenericRepository<Product> productsRepository,
            IGenericRepository<ProductBrand> productBrandsRepository,
            IGenericRepository<ProductType> productTypesRepository, IMapper mapper)
        {
            _productsRepository = productsRepository;
            _productBrandsRepository = productBrandsRepository;
            _productTypesRepository = productTypesRepository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IReadOnlyList<ProductToReturnDto>>> GetProducts()
        {
            var specifications = new ProductsWithTypesAndBrandsSpecification();

            var products = await _productsRepository.ListAsync(specifications);

            return Ok(
                _mapper.Map<IReadOnlyList<Product>, IReadOnlyList<ProductToReturnDto>>(products));
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductToReturnDto>> GetProduct(int id)
        {
            var specifications = new ProductsWithTypesAndBrandsSpecification(id);

            var product = await _productsRepository.GetEntityWithSpecification(specifications);

            return _mapper.Map<Product, ProductToReturnDto>(product);
        }

        [HttpGet("brands")]
        public async Task<ActionResult<IReadOnlyList<ProductBrand>>> GetProductBrands()
        {
            var productBrands = await _productBrandsRepository.ListAllAsync();
            return Ok(productBrands);
        }

        [HttpGet("types")]
        public async Task<ActionResult<IReadOnlyList<ProductType>>> GetProductTypes()
        {
            var productTypes = await _productTypesRepository.ListAllAsync();
            return Ok(productTypes);
        }

    }
}
