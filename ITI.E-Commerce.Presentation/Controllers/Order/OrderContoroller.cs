using Castle.Core.Resource;
using ITI.E_Commerce.Models;
using ITI.E_Commerce.Presentation.IRepository;
using ITI.E_Commerce.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Security.Cryptography;

namespace ITI.E_Commerce.Presentation.Controllers
{
    public class OrderController : Controller
    {

        private readonly IOrderRepository orderRepository;

        public OrderController(IOrderRepository _OrderRepository)
        {
            orderRepository = _OrderRepository;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> GetAll()
        {
            ViewBag.Title = "All Orders";
            ViewBag.AllCustomers = await orderRepository.GetAllCustomers();
            ViewBag.AllShippers = await orderRepository.GetAllShippers();
            ViewBag.AllAddresses = await orderRepository.GetAddresses();

            var Orders = await orderRepository.GetAllOrder();
            return View(Orders);


        }

        #region Add Order Commented
        //[HttpGet]
        //public IActionResult Add()
        //{
        //    ViewBag.AllCustomers = orderRepository.GetAllCustomers();
        //    ViewBag.AllShippers = orderRepository.GetAllShippers();
        //    ViewBag.AllAddresses = orderRepository.GetAddresses();
        //    return View();
        //}
        //[HttpPost]
        //public async Task<IActionResult> Add(OrderCreateModel model)
        //{
        //    var Orders = await orderRepository.CreateNew(model);
        //    return RedirectToAction("GetAll");


        //} 
        #endregion


        [HttpGet]
        [Authorize]
        public async Task<IActionResult> Edit(int id)
        {
            ViewBag.AllCustomers = await orderRepository.GetAllCustomers();
            ViewBag.AllShippers = await orderRepository.GetAllShippers();
            ViewBag.AllAddresses = await orderRepository.GetAddresses();
            ViewBag.Order = await orderRepository.Get(id);

            return View();
        }

        [HttpPost]
        [Authorize]
        public async Task<IActionResult> Edit(int id, Order ord)
        {


            ViewBag.Order = await orderRepository.Get(id);
            if (ModelState.IsValid == false)
            {

                ViewBag.Order = await orderRepository.Get(id);
                return View();
            }
            else
            {
                orderRepository.Edit(ord, id);
                return RedirectToAction("GetAll");
            }
        }

        [Authorize]
        public IActionResult ConfirmDelete(int id)
        {
            dynamic Order = new ExpandoObject();
            Order.ID = id;
            return View(Order);
        }
        [HttpGet]
        [Authorize]
        public IActionResult Delete(int id)
        {
            orderRepository.Delete(id);
            return RedirectToAction("GetAll");
        }
    }
}
