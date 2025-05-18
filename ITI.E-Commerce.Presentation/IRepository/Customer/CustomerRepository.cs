using ITI.E_Commerce.Presentation.Models;
using ITI.E_Commerce.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;
using System.Collections.Generic;

namespace ITI.E_Commerce.Presentation.IRepository
{
    public class CustomerRepository : CustomerIRepository
    {
        private readonly MyContext mycontext;
        private readonly SignInManager<Customer> SignInManager;
        private readonly RoleManager<IdentityRole> RoleManager;
        private readonly UserManager<Customer> UserManager;

        public CustomerRepository(
            UserManager<Customer> _UserManager,
            SignInManager<Customer> _SignInManager,
            RoleManager<IdentityRole> roleManager,
            MyContext _mycontext)
        {
            UserManager = _UserManager;

            SignInManager = _SignInManager;
            RoleManager = roleManager;
            mycontext = _mycontext;
        }
        public IQueryable<SelectListItem> get_al_lRoles()
        {
            return RoleManager.Roles
                .Where(i => i.Name != "Admin")
              .Select(i => new SelectListItem(i.Name, i.Name));
        }
        public IQueryable<SelectListItem> get_al_lRole()
        {
            return RoleManager.Roles
                .Where(i => i.Name != "customer")
              .Select(i => new SelectListItem(i.Name, i.Name));
        }
        public async Task<IdentityResult> CreateCustomer(UserCreateModel obj)
        {
            Customer user = new Customer()
            {
                FirstName = obj.FirstName,
                LastName = obj.LastName,
                UserName = obj.UserName,
                Email = obj.Email,
                PhoneNumber = obj.PhoneNumber,

            };
            IdentityResult result = await UserManager.CreateAsync(user, obj.Password);
            if (result.Succeeded == true)
            {
                await UserManager.AddToRoleAsync(user, obj.Role);
            }
                return  result;
        }
        public async Task<Microsoft.AspNetCore.Identity.SignInResult> SignInCustomer(LoginModel model)
        {
            return await SignInManager.PasswordSignInAsync
                        (model.UserName, model.Password, model.RememberMe,
                             true);
        }
        public async void SignOutCustomer() {
            await SignInManager.SignOutAsync();
        }
        public async Task<IdentityResult> ChangePassword(ChangePasswordModel obj,ClaimsPrincipal User)
        {
          
            var user = await UserManager.GetUserAsync(User);
            var c = await UserManager.ChangePasswordAsync(user, obj.CurrentPassword,
               obj.NewPassword);
            return c;
        }
        public async Task<IdentityResult> ConfirmEmailCustomer(string uid, string token)
        {

            token = token.Replace(' ', '+');
            var user = await UserManager.FindByIdAsync(uid);
           return await UserManager.ConfirmEmailAsync(user, token);
        }
        public void DeleteCustomer(string id)
        {
            IQueryable<Order> order_user = mycontext.Orders.Where(i => i.CustomerID== id);
            foreach (var order in order_user.ToList())
            {
                IQueryable<OrderDetails> order_details_user = mycontext.orderDetails.Where(i => i.OrderID == order.ID);
                foreach (var order_details in order_details_user.ToList())
                {

                    mycontext.orderDetails.Remove(order_details);
                }
                    mycontext.Orders.Remove(order);
            }
            var customer = mycontext.Customers.FirstOrDefault(i => i.Id == id);
            mycontext.Customers.Remove(customer);
            mycontext.SaveChanges();
        }
        public async Task<bool> CheckRole(LoginModel model)
        {
          Customer customer = mycontext.Customers.FirstOrDefault(i => i.UserName == model.UserName);
          bool check= await  UserManager.IsInRoleAsync(customer, "customer");
            return check;
        }
        public async Task<IEnumerable<Customer>> GetAllInterFaceCustomers()
        {
            //mycontext.Customers.ToList();
            //IEnumerable<Customer> all_Users = new List<Customer>();
            //List<Customer> Admins = (List<Customer>)await UserManager.GetUsersInRoleAsync("Admin");
            List<Customer> customers = (List<Customer>)await UserManager.GetUsersInRoleAsync("customer");
            //List<Customer> shippers = (List<Customer>)await UserManager.GetUsersInRoleAsync("bya3");
            //all_Users = customers;
            return customers;
        }

        public async Task<IEnumerable<Customer>> GetAllAdmins()
        {
            List<Customer> Admins = (List<Customer>)await UserManager.GetUsersInRoleAsync("Admin");
            return Admins;
        } 
        
        public async Task<IEnumerable<Customer>> GetAllShippers()
        {
            List<Customer> Shippers = (List<Customer>)await UserManager.GetUsersInRoleAsync("bya3");
            return Shippers;
        }


        public async Task<Customer> UpdateInterFAceCustomer(ClaimsPrincipal User)
        {
            return (Customer)await UserManager.GetUserAsync(User);
        }
        public void UpdateCustomer(UserUpdateModel obj,Customer customer)
        {
            customer.FirstName = obj.FirstName;
            customer.LastName = obj.LastName;
            customer.UserName = obj.username;
            customer.Email = obj.email;
            customer.PhoneNumber = obj.PhoneNumber;
            mycontext.SaveChanges();
        }
        public async Task <List<string>> GetAllRoleUser(Customer User)
        {
            return  (List<string>)await UserManager.GetRolesAsync(User);
        }
        public async Task<Boolean>  UpdateRoleUser(Customer user, RoleUpdateModel model)
        {
            //IEnumerable<string> roles = await UserManager.GetRolesAsync(user);
            IEnumerable<string> rolename = await UserManager.GetRolesAsync(user);
          var test =  await UserManager.RemoveFromRolesAsync(user, rolename);
          var test2 = await UserManager.AddToRoleAsync(user, model.Name);
            var v = test;
            var n = test2;
            return true;
            
        }
        public Customer GetCustomerId(String Id)
        {
            return mycontext.Customers
                .FirstOrDefault(i => i.Id == Id);
        }

        
    }
}
