using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using ITI.E_Commerce.Api.Helper;
using ITI.E_Commerce.Api.Interfaces;
using ITI.E_Commerce.Api.Model;
using ITI.E_Commerce.Api.Specifications;
using ITI.E_Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.CodeAnalysis.Differencing;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ITI.E_Commerce.Api.Controllers
{

    [Route("api/[controller]/[action]")]
    [ApiController]
    public class ProductController : ControllerBase
    {
        private readonly IGenericRepository<Product> productRepo;
        private readonly IGenericRepository<Brand> productBrandRepo;
        private readonly IGenericRepository<Category> productTypeRepo;
        private readonly IMapper mapper;

        public ProductController(IGenericRepository<Product> productRepo,
            IGenericRepository<Brand> productBrandRepo,
            IGenericRepository<Category> productTypeRepo,
            IMapper mapper

            )
        {
            this.productRepo = productRepo;
            this.productBrandRepo = productBrandRepo;
            this.productTypeRepo = productTypeRepo;
            this.mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<Pagination<ProductModel>>> GetProducts([FromQuery] ProductFilterParam productParams)
        {
            var specs = new ProductsWithCategoryAndBrandsSpecification(productParams);
            var countSpecs = new ProductsWithFiltersCount(productParams);
            var totalItems = await productRepo.CountAsync(countSpecs);
            var products = await productRepo.GetAllAsync(specs);
            var data = mapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(products);

            var reponse = new Pagination<ProductModel>()
            {
                Count = totalItems,
                PageSize = productParams.PageSize,
                PageIndex = productParams.PageIndex,
                Data = data
            };

            return Ok(reponse);
        }


        [HttpGet]
        public async Task<ActionResult<ProductModel>> GetAllProducts()
        {
            var specs = new ProductsWithCategoryAndBrandsSpecification();
            var products = await productRepo.GetAllAsync(specs);
            var data = mapper.Map<IEnumerable<Product>, IEnumerable<ProductModel>>(products);

            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<ProductModel>>GetProduct(int id)
        {
            var specs = new ProductsWithCategoryAndBrandsSpecification(id);
            var product = await productRepo.GetByIdAsync(id, specs);

            if(product == null)
            {
                return NotFound();
            }
            var data = mapper.Map<Product, ProductModel>(product);

              return Ok(data);
        }
        [HttpGet]
        public async Task<ActionResult<List<CategoryModel>>> GetTypesCount()
        {   
            var param = new ProductFilterParam() { PageSize = 50 };
            var specs = new ProductsWithCategoryAndBrandsSpecification(param);
            var result = await productRepo.GetAllAsync(specs);
            var count = result.GroupBy(p => p.Category).Select(g => new CategoryModel { id = g.Key.Id, Name = g.Key.Name, Count = g.Count() }).ToList();
            count.Add(new()
            {
                Name = "All",
                id = 0,
                Count = result.Count()
            });
            return Ok(count);
        }

        [HttpGet]
        public async Task<ActionResult<List<BrandModel>>> GetBrandCount()
        {
            var param = new ProductFilterParam() { PageSize = 50 };
            var specs = new ProductsWithCategoryAndBrandsSpecification(param);
            var result = await productRepo.GetAllAsync(specs);
            var count = result.GroupBy(p => p.Brand).Select(g => new BrandModel { id = g.Key.Id, Name = g.Key.Name, Count = g.Count() }).ToList();
            count.Add(new()
            {
                Name = "All",
                id = 0,
                Count = result.Count()
            });
            return Ok(count);
        }

        [HttpGet]
        public async Task<ActionResult<decimal>> GetMaxPrice()
        {
            //var specs = new ProductsWithTypesAndBrandsSpecification(new ProductSpecParams());
            var result = await productRepo.GetAllAsync();
            var maxPrice = result.Max(p => p.UnitPrice);

            return Ok(maxPrice);
        }





    }
}

