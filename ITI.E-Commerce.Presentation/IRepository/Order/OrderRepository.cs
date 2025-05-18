using System;
using ITI.E_Commerce.Models;
using ITI.E_Commerce.Presentation.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Cryptography;
using Castle.Core.Resource;
using System.Reflection;
using Microsoft.AspNetCore.Identity;

namespace ITI.E_Commerce.Presentation.IRepository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly MyContext context;
        private readonly IWebHostEnvironment Webhost;
        private readonly IConfiguration configuration;
        private readonly UserManager<Customer> UserManager;
        public OrderRepository(IWebHostEnvironment _webhost, MyContext _context, IConfiguration _configuration, UserManager<Customer> userManager)
        {
            context = _context;
            Webhost = _webhost;
            configuration = _configuration;
            UserManager = userManager;
        }
        public async Task<Order> CreateNew(OrderCreateModel model)
        {


            var newOrder = new Order()

            {
                OrderDate = DateTime.Now,
                //CustomerID = model.CustomerID,

                //ShipperID = model.ShipperID,

                //ShipperID = model.ShipperID,
                AddressID = model.AddressID,

                ShippedDate = model.ShippedDate,
                //ShippingAddress = model.ShippingAddress,
                DeliveryCost = model.DeliveryCost,
                //TotalPrice = model.TotalPrice,
                //OrderDetails = model.orderDetails

            };
            await context.Orders.AddAsync(newOrder);

            context.SaveChanges();

            return newOrder;
        }
        public void Edit(Order ord, int id)
        {
            Order order = context.Orders.FirstOrDefault(i => i.ID == id);

            order.ShipperID = ord.ShipperID;
            order.ShippedDate = ord.ShippedDate;
            order.Status = ord.Status;
            order.AddressID = ord.AddressID;
            order.DeliveryCost = ord.DeliveryCost;
            order.TotalPrice += (decimal)ord.DeliveryCost;
            //order.OrderDate = ord.OrderDate;
            //order.CustomerID = ord.CustomerID;
            //order.ShippingAddress = ord.ShippingAddress;
            //order.TotalPrice = ord.TotalPrice;


            context.SaveChanges();

        }

        public void Delete(int id)
        {
            var Order = context.Orders.FirstOrDefault(i => i.ID == id);
            IQueryable<OrderDetails> order_details_user = context.orderDetails.Where(i => i.OrderID == Order.ID);
            foreach (var order_details in order_details_user.ToList())
            {

                context.orderDetails.Remove(order_details);
            }
            context.Orders.Remove(Order);
            context.SaveChanges();
        }

        public async Task<List<Order>> GetAllOrder()
        {
            var orders = await context.Orders.ToListAsync();
            return orders;
        }

        public Task<Order> Get(int id)
        {
            var order = context.Orders.FirstOrDefaultAsync(p => p.ID == id);
            return order;
        }

        public async Task<IQueryable<SelectListItem>> GetAllCustomers()
        {
            var Customers = context.Customers.Select(i => new SelectListItem(i.FirstName, i.Id.ToString())).AsNoTracking();
            return Customers;
        }


        public async Task<IQueryable<SelectListItem>> GetAddresses()
        {
            var Addresses = context.Addresses.Select(i => new SelectListItem(i.City, i.ID.ToString())).AsNoTracking();
            return Addresses;

        }


        public async Task<IEnumerable<SelectListItem>> GetAllShippers()
        {
            //var Shippers = context.Customers.Select(i => new SelectListItem(i.FirstName, i.Id.ToString())).AsNoTracking();
            IEnumerable<Customer> Shippers = (List<Customer>)await UserManager.GetUsersInRoleAsync("bya3");

            //var Shippers = context.Customers;
            //foreach(var shipper in Shippers)
            //{
            //    context.Customers
            //}
            return Shippers.Select(i => new SelectListItem(i.FirstName, i.Id.ToString()));
        }

    }
}
