using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ITI.E_Commerce.Models;
using ITI.E_Commerce.Presentation.IRepository;
using ITI.E_Commerce.Presentation.Models;
using ITI.E_Commerce.Presentation.Models.CategoryModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using X.PagedList;
// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ITI.E_Commerce.Presentation.Controllers
{
    public class CategoryController : Controller
    {


        private readonly ICategoryRepository categoryRepository;
        public CategoryController(ICategoryRepository categoryRepository)
        {
            this.categoryRepository = categoryRepository;
        }

        //[Authorize(Roles = "Administrator")]
        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult AddNewCategory()
        {
            ViewBag.Title = "Add New Category";
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult AddNewCategory(CategoryModel category)
        {
            if (ModelState.IsValid == false)
            {
                var errors =
                        ModelState.SelectMany(i => i.Value.Errors.Select(x => x.ErrorMessage));
                foreach (string err in errors)
                    ModelState.AddModelError("", err);
                    ViewBag.Success = false;
                return View();
            }

            else
            {
                ViewBag.Success = true;

                categoryRepository.Insert(category);
            }
            return View();
        }

        [HttpGet ]
        public IActionResult GetAll(int pageIndex = 1, int pageSize = 4)
        {
            ViewBag.Title = "All Categories";
            var categories = categoryRepository.GetAll().ToPagedList(pageIndex, pageSize);
            return View(categories);
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Delete(int id)
        {
            categoryRepository.Delete(id);
            return RedirectToAction("GetAll");
        }

        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(int id)
        {
            ViewBag.Title = "Update Category";
            ViewBag.Category = categoryRepository.GetById(id);
            return View();
        }

        [HttpPost]
        [Authorize(Roles = "Admin")]
        public IActionResult Update(CategoryModel model ,Category category)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.Category = category;
                return View();
            }
            else
            {
                //var category = categoryRepository.GetById(model.Id);
                categoryRepository.Update(model,category);
                return View();
            }
        }



    }
}


