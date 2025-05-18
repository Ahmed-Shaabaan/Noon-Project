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
    public class BrandController : Controller
    {
        private readonly IGenericRepository<Brand> productBrandRepo;
        private readonly IMapper mapper;
        public BrandController(IMapper _mapper, IGenericRepository<Brand> _productBrandRepo)
        {
            productBrandRepo = _productBrandRepo;
            mapper = _mapper;
        }
        [HttpGet]
        public async Task<ActionResult<Pagination<BrandModel>>> GetAllBrand([FromQuery] ProductFilterParam productParams)
        {
            var specs = new ProductBrandSpecification(productParams);

            var countSpecs = new ProductBrandSpecification(productParams);

            var totalItems = await productBrandRepo.CountAsync(countSpecs);

            var productstypes = await productBrandRepo.GetAllAsync(specs);

            var data = mapper.Map<IReadOnlyList<Brand>, IReadOnlyList<BrandModel>>(productstypes);

            var reponse = new Pagination<BrandModel>()
            {
                Count = totalItems,
                PageSize = productParams.PageSize,
                PageIndex = productParams.PageIndex,
                Data = data
            };

            return Ok(reponse);

        }



        [HttpGet]
        public async Task<ActionResult<BrandModel>> AllBrands()
        {

            var productstypes = await productBrandRepo.GetAllAsync();

            var data = mapper.Map<IReadOnlyList<Brand>, IReadOnlyList<BrandModel>>(productstypes);

            return Ok(data);

        }
        [HttpGet("{id}")]
        public async Task<ActionResult<BrandModel>> GetProductBrand(int id)
        {
            var specs = new ProductBrandSpecification(id);

            var productBrand = await productBrandRepo.GetByIdAsync(id, specs);

            return Ok(mapper.Map<Brand, BrandModel>(productBrand));
        }




    }
}

