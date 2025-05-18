
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using ITI.E_Commerce.Presentation.Models;
using ITI.E_Commerce.Presentation.IRepository;
using System.Security.Claims;
using ITI.E_Commerce.Models;

namespace ITI.E_Commerce.Presentation.controller
{
    public class UserController : Controller
    {
        private readonly CustomerIRepository productRepository;
        public UserController(CustomerIRepository _productRepository)
        {
            productRepository = _productRepository;
        }
        [HttpGet]
        public IActionResult SignUp()
        {
            ViewBag.Title = "Sign Up";
            ViewBag.Roles = productRepository.get_al_lRoles();

            return View();
        }
        [HttpPost]
        public async Task<IActionResult> SignUp(UserCreateModel model)
        {
            if (ModelState.IsValid == false)
            {
                ViewBag.Roles =  productRepository.get_al_lRoles(); 
                return View();
            }
            else
            {
                IdentityResult result = await productRepository.CreateCustomer(model);               
                 if (result.Succeeded == false)
                {
                    result.Errors.ToList().ForEach(i =>
                    {
                        ModelState.AddModelError("", i.Description);
                    });
                    ViewBag.Roles = productRepository.get_al_lRoles();
                    return View();
                }
                else
                {
                    return RedirectToAction("SignIn", "User");
                }
            }
        }
        
        [HttpGet]
        public IActionResult SignIn(string ReturnUrl = null)
        {
            ViewBag.Title = "Sign In";

            ViewBag.ReturnUrl = ReturnUrl;
            return View();
        }
        
        [HttpPost]
        public async Task<IActionResult> SignIn(LoginModel model)
        {
            if (ModelState.IsValid == false)
                return View();
            else
            {
               bool CheckRole =await productRepository.CheckRole(model);
                if (CheckRole==false)
                {
                    var result
                     = await productRepository.SignInCustomer(model);
                    if (result.Succeeded == false)
                    {
                        ModelState.AddModelError("", "Invalid User Name Of Password");
                        return View();
                    }
                    else if (result.IsLockedOut == true)
                    {
                        ModelState.AddModelError("", "You're Locked Out Please Try Again After 20 Minute");
                        return View();
                    }
                    else
                    {
                        if (!string.IsNullOrEmpty(model.ReturnUrl))
                            return LocalRedirect(model.ReturnUrl);
                        else
                            return RedirectToAction("Index", "Home");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "You donot have permission ");
                    return View();
                }
               
            }
        }
        
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> SignOut()
        {
            ViewBag.Title = "Sign Out";
            productRepository.SignOutCustomer();
            return RedirectToAction("SignIn", "User");
        }
        
        [HttpGet]
        public IActionResult NotAuthorized()
        {
            ViewBag.Title = "NotAuthorized";
            return View();
        }
        
        [Authorize]
        [HttpGet]
        public IActionResult ChangePassword()
        {
            ViewBag.Title = "Change Password";
            return View();
        }
        
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> ChangePassword(ChangePasswordModel model)
        {
            if (ModelState.IsValid == false) return View();
            else
            {
                var c = await productRepository.ChangePassword(model, User);
                if (c.Succeeded)
                {
                    return View();
                }
                else
                {
                    return View();
                }
            }
        }
        
        [HttpGet]
        public async Task<IActionResult> ConfirmEmail(string uid, string token)
        {
            ViewBag.Title = "Confirm Email";
            await productRepository.ConfirmEmailCustomer( uid,  token);
            return RedirectToAction("Index", "Book");
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public IActionResult Delete(string id)
        {
            ViewBag.Title = "Remove Customer";
            productRepository.DeleteCustomer(id);
            return RedirectToAction("GetAllCustomers");
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllCustomers()
        {
            ViewBag.Title = "All Customers";
            var customer = await productRepository.GetAllInterFaceCustomers();
            return View(customer );
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllAdmins()
        {
            ViewBag.Title = "All Admins";
            var admins = await productRepository.GetAllAdmins();
            return View(admins);
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> GetAllShippers()
        {
            ViewBag.Title = "All Shippers";
            var shippers = await productRepository.GetAllShippers();
            return View(shippers);
        }
        
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> UpdateCustomer()
        {
            ViewBag.Title = "Update Information";
            ViewBag.customer = await productRepository.UpdateInterFAceCustomer(User);
            return View("UpdateCustomer");
        }

        [Authorize]
        [HttpPost]
        public async Task<IActionResult> UpdateCustomer( UserUpdateModel model)
        {
            if (ModelState.IsValid == false)
            {
                return RedirectToAction("UpdateCustomer");
            }
            else
            {
                var customer = await productRepository.UpdateInterFAceCustomer(User);
                if (customer == null)
                {
                    return RedirectToAction("UpdateCustomer");
                }
                productRepository.UpdateCustomer(model, customer);
                return View("UpdateCustomer");
            }
        }
        
        [Authorize(Roles = "Admin")]
        [HttpGet]
        public async Task<IActionResult> UpdateRole(string id)
        {
            ViewBag.Title = "Update Role";
            var user =  productRepository.GetCustomerId(id);
            ViewBag.MyRole = await productRepository.GetAllRoleUser(user);
            ViewBag.Roles = productRepository.get_al_lRole();
            ViewBag.Id = user.Id;
            return View();
        }
        
        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> UpdateRole(string id, RoleUpdateModel model)
        {
            Customer userv =  productRepository.GetCustomerId(id);
            if (userv == null)
            {
                ModelState.AddModelError("", "Not Found");
                return View("UpdateRole");
            }
            await productRepository.UpdateRoleUser(userv, model);
            ViewBag.MyRole = await productRepository.GetAllRoleUser(userv);
            ViewBag.Roles = productRepository.get_al_lRole();
            ViewBag.Id = userv.Id;
            return View("UpdateRole");
        }
    }

}
