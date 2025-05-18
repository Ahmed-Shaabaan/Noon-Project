using ITI.E_Commerce.Models;
using ITI.E_Commerce.Presentation.IRepository;
using ITI.E_Commerce.Presentation.IRepository.BrandR;
using ITI.E_Commerce.Presentation.Models;
using ITI.E_Commerce.Presentation.Models.CategoryModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
using System;
using System.Data;
using System.IO;
using System.Linq;
using X.PagedList;
namespace ITI.E_Commerce.Presentation.Controllers
{
    public class BrandController : Controller
    {
        private readonly IStringLocalizer<BrandController> _localizer;
        private readonly IBrandRepository brandRepository;

        public BrandController(IBrandRepository brandRepository, IStringLocalizer<BrandController> stringLocalizer = null)
        {
            this.brandRepository = brandRepository;
            _localizer = stringLocalizer;
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddNewBrand()
        {
            ViewBag.Title = "Add New Brand";
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddNewBrand(BrandCreateModel brand)
        {
            

            if (ModelState.IsValid == false)
            {
                var errors =
                        ModelState.SelectMany(i => i.Value.Errors.Select(x => x.ErrorMessage));
                foreach (string err in errors)
                    ModelState.AddModelError("", err);

                ViewBag.Success = false;
                ViewBag.Title = "Brand";
                return View();
            }
            else
            {
                brandRepository.Insert(brand);
            }
            return RedirectToAction("AllBrand");

        }

        [HttpGet]
        public IActionResult AllBrand(int pageIndex = 1, int pageSize = 4)
        {
            ViewBag.Title = "All Brands";

            var brands = brandRepository.GetAll().ToPagedList(pageIndex, pageSize);
            return View(brands);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            ViewBag.Title = "Delete Brand";
            brandRepository.Delete(id);
            return RedirectToAction("AllBrand");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {
            ViewBag.Title = "Delete Brand";

            ViewBag.Brand = brandRepository.GetById(id);
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(BrandCreateModel model, int id)
        {
            Brand brand = brandRepository.GetById(id);
            if (ModelState.IsValid == false)
            {
                ViewBag.Brand = brand;
                return View();
            }
            else
            {
                //var category = categoryRepository.GetById(model.Id);
                brandRepository.Update(model, brand);
                return View("AllBrand");

            }
        }
    }
}
