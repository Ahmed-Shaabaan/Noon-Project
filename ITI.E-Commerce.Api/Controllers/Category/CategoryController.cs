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
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ITI.E_Commerce.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    [ApiController]
    public class CategoryController : Controller
    {
        private readonly IGenericRepository<Category> productCategoryRepo;
        private readonly IMapper mapper;
        public CategoryController(IMapper _mapper, IGenericRepository<Category> _productBrandRepo)
        {
            productCategoryRepo = _productBrandRepo;
            mapper = _mapper;
        }
        [HttpGet]
         public async Task<ActionResult<Pagination<CategoryModel>>> GetAllCategory([FromQuery] ProductFilterParam productParams)
        {
            var specs = new ProductCategorySpecification(productParams);

            var countSpecs = new ProductCategorySpecification(productParams);

            var totalItems = await productCategoryRepo.CountAsync(countSpecs);

            var categories = await productCategoryRepo.GetAllAsync(specs);

            var data = mapper.Map<IReadOnlyList<Category>, IReadOnlyList<CategoryModel>>(categories);

            var reponse = new Pagination<CategoryModel>()
            {
                Count = totalItems,
                PageSize = productParams.PageSize,
                PageIndex = productParams.PageIndex,
                Data = data
            };

            return Ok(reponse);

        }

        [HttpGet]
        public async Task<ActionResult<CategoryModel>> AllCategories()
        {
            var categories = await productCategoryRepo.GetAllAsync();
            var data = mapper.Map<IReadOnlyList<Category>, IReadOnlyList<CategoryModel>>(categories);
            return Ok(data);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<CategoryModel>> GetProductCategory(int id)
        {
            var specs = new ProductCategorySpecification(id);

            var productBrand = await productCategoryRepo.GetByIdAsync(id, specs);

            return Ok(mapper.Map<Category, CategoryModel>(productBrand));
        }




    }
}

