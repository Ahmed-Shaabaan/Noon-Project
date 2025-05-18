using System;
using System.Collections.Generic;
using System.Data;
using System.Dynamic;
using System.Linq;
using System.Threading.Tasks;
using ITI.E_Commerce.Models;
using ITI.E_Commerce.Presentation.IRepository;
using ITI.E_Commerce.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using X.PagedList;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ITI.E_Commerce.Presentation.Controllers
{
    [Authorize(Roles = "Admin")]
    public class ProductController : Controller
    {
        
        private readonly IProductRepository productRepository;
        public ProductController (IProductRepository _productRepository)
        {
            productRepository = _productRepository;
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            ViewBag.Title = "Add Product";
            ViewBag.Brand = productRepository.GetAllBrand();
            ViewBag.Category = productRepository.GetAllCategory();
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task <IActionResult> Add(ProductCreateModel model)
        {
             var products =  await productRepository.CreateNew(model);
            return RedirectToAction("GetAllProduct");
        }

        [HttpGet]
        public async Task<IActionResult> Get(int id)
        {
            ViewBag.Title = "Product Page";
            var product = await productRepository.Get(id);
            ViewBag.Brand = productRepository.GetAllBrand();
            ViewBag.Category = productRepository.GetAllCategory();

            return View(product);

        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(int id)
        {
            ViewBag.Title = "Update Product";
            ViewBag.Product = await  productRepository.Get(id);
            ViewBag.Brand = productRepository.GetAllBrand();
            ViewBag.Category = productRepository.GetAllCategory();

            return View();
        }
        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Update(Product product, int id)
        {
            ViewBag.Product =await productRepository.Get(id);
            if (ModelState.IsValid == false)
            {

                ViewBag.Product =  productRepository.Get(id);
                return View();
            }
            else
            {
                //ViewBag.Product = await productRepository.Get(id);
                productRepository.Update(product, id);
                return RedirectToAction("GetAllProduct");
            }
        }
        [HttpGet]
        public  IActionResult GetAllProduct(int pageIndex = 1, int pageSize = 4)
        {
            ViewBag.Title = "All Product";
            var products =  productRepository.GetAllProduct().ToPagedList(pageIndex, pageSize);
            return View(products);
        }
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            ViewBag.Title = "Delete Product";
            productRepository.Delete(id);
            return RedirectToAction("GetAllProduct");
        }
        [Authorize(Roles = "Admin")]
        public IActionResult ConfirmDelete(int id, string name)
        {
            ViewBag.Title = "Confirm Delete";
            dynamic Product = new ExpandoObject();
            Product.Id = id;
            Product.Name = name;
            return View(Product);
        }


    }
}

