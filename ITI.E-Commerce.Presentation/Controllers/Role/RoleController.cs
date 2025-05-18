using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using ITI.E_Commerce.Presentation.Models;

namespace ITI.E_Commerce
{
   [Authorize(Roles = "Admin")]
    public class RoleController : Controller
    { 

        RoleManager<IdentityRole> RoleManager;
        public RoleController(RoleManager<IdentityRole> roleManager)
        {
            RoleManager = roleManager;
        }


        [HttpGet]
        [Authorize(Roles = "Admin")]
        public IActionResult Add()
        {
            ViewBag.Title = "Add Role";
            return View();
        }


        [HttpPost]
        [Authorize(Roles = "Admin")]
        public async Task<IActionResult> Add(RoleCreateModel model)
        {

            if (ModelState.IsValid == false)
                return View();
            else
            {
                var result  = 
                await RoleManager.CreateAsync(
                    new IdentityRole
                    {
                        Name=model.Name
                    });
                if(result.Succeeded == false)
                {
                    result.Errors.ToList().ForEach(i =>
                    {
                        ModelState.AddModelError("", i.Description);
                    });
                    return View();
                }
                else
                {
                    return View();
                }

            }
        }
    }
}
