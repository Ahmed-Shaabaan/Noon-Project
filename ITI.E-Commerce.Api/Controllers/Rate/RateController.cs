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

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ITI.E_Commerce.Api.Controllers
{

    [Route("api/[controller]/[action]")]
    public class RateController : Controller
    {
        private readonly IGenericRepository<Rate> reviewRepo;
        private readonly IMapper mapper;
        private readonly IConfiguration config;
        public RateController(IMapper _mapper, IGenericRepository<Rate> _reviewRepo, IConfiguration _config)
        {
            mapper = _mapper;   
            reviewRepo = _reviewRepo;
            config = _config;
        }


        [HttpGet("{id}")]
        public async Task<IActionResult> getReviews(int id)
        {
            var specs = new RateSpecification(id);
            var reviews = await reviewRepo.GetAllAsync(specs);
            var data = mapper.Map<IReadOnlyList<Rate>, IReadOnlyList<RateModel>>(reviews);

            specs.GetTotalReviews(reviews);
            var rating = specs.CalculateRating();

            return Ok(new
            {
                reviews = data,
                rating = rating,
                stars = specs.getStars(),
            });
        }


        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpPost]
        public async Task<IActionResult> AddReview([FromBody]AddRateModel addReviewDto)
        {
            var userId = TokenExtractor.GetUserId(config, HttpContext);
            await reviewRepo.AddAsync(new Rate
            {
                ProductID = addReviewDto.productId,
                Comment = addReviewDto.Comment,
                CustomerID = userId,
                Date = DateTime.Now,
                NumberOfStart = addReviewDto.stars,
            });

            var specs = new RateSpecification(addReviewDto.productId);

            var reviews = await reviewRepo.GetAllAsync(specs);

            var data = mapper.Map<IReadOnlyList<Rate>, IReadOnlyList<RateModel>>(reviews);

            specs.GetTotalReviews(reviews);
            var rating = specs.CalculateRating();

            return Ok(new
            {
                reviews = data,
                rating = rating,
                stars = specs.getStars(),
            });
        }

    }
}

