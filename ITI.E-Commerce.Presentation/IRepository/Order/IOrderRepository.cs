using ITI.E_Commerce.Models;
using ITI.E_Commerce.Presentation.Models;
using Microsoft.AspNetCore.Mvc.Rendering;



namespace ITI.E_Commerce.Presentation.IRepository
{
    public interface IOrderRepository
    {

        public Task<List<Order>> GetAllOrder();
        public Task<IQueryable<SelectListItem>> GetAllCustomers();

        public Task<IEnumerable<SelectListItem>> GetAllShippers();
        public Task<IQueryable<SelectListItem>> GetAddresses();

        public void Edit(Order order, int id);
        public void Delete(int id);
        public Task<Order> CreateNew(OrderCreateModel obj);
        public Task<Order> Get(int id);
        
    }
}
