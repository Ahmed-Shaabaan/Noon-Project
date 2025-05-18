using ITI.E_Commerce.Api.Model;
using ITI.E_Commerce.Api.Repositories;
using ITI.E_Commerce.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ITI.E_Commerce.Api.Controllers
{
    [Route("api/[controller]/[action]")]
    
    [ApiController]
    public class OrderController : ControllerBase
    {
        private readonly IOrderRepositoryApi orderRepository;
        public OrderController(IOrderRepositoryApi _orderRepository)
        {
            orderRepository = _orderRepository;
        }
        //[Authorize]
        // POST create/<OrderController>
        [HttpPost]
        public async Task<GetData> Create_Order([FromBody] OrderCreateModelApi order)
        {
                return await orderRepository.CreateNew(order);
        }
        // POST api/<OrderController>
        [Authorize]
        [HttpPost]
        public async Task<GetData> Update_Order(int id, [FromBody] OrderCreateModelApi order)
        {
            return await orderRepository.Edit(id, order);
        }
        //[Authorize]
        //// PUT api/<OrderController>/5
        [HttpGet]
        public GetData get_order(string id_user)
        {
            return orderRepository.Get(id_user);
        }
        // DELETE api/<OrderController>/5
        [Authorize]
        [HttpDelete]
        public GetData Delete(int id)
        {
            return orderRepository.Delete(id);
        }
        //[Authorize]
        [HttpGet]
        public IEnumerable<ProductModel> get_Product_best_seller()
        {
            return orderRepository.get_best_seller();
        }
    }
}
