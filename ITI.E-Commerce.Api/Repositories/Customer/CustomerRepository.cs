using Castle.Core.Resource;
using ITI.E_Commerce.Api.Model;
using ITI.E_Commerce.Models;
using ITI.E_Commerce.Presentation.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json.Linq;
using NuGet.Common;
using System.Data;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ITI.E_Commerce.Api.IRepository
{
    public class CustomerRepositoryd : ICustomerRepository
    {
        private readonly MyContext mycontext;
        private readonly SignInManager<Customer> SignInManager;
        private readonly RoleManager<IdentityRole> RoleManager;
        private readonly UserManager<Customer> UserManager;

        public CustomerRepositoryd(UserManager<Customer> _UserManager,
            SignInManager<Customer> _SignInManager,
            RoleManager<IdentityRole> roleManager,
            MyContext _mycontext)
        {
            UserManager = _UserManager;

            SignInManager = _SignInManager;
            RoleManager = roleManager;
            mycontext = _mycontext;
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
            return result;
        }

        public async Task<IdentityResult> ChangePassword(ChangePasswordModel obj, Customer User)
        {
            var c = await UserManager.ChangePasswordAsync(User, obj.CurrentPassword, obj.NewPassword);
            return c;
        }
        [HttpPut]
        public ResultViewModel UpdateCustomer(UserUpdateModel obj, Customer customer)
        {
            customer.FirstName = obj.FirstName;
            customer.LastName = obj.LastName;
            customer.UserName = obj.username;
            customer.Email = obj.email;
            customer.PhoneNumber = obj.PhoneNumber;
            mycontext.SaveChanges();
            ResultViewModel myModel = new ResultViewModel()
            {
                Success = true,
                Message = "Update Information User",
                email = customer.Email,
                username = customer.UserName,
                FirstName = customer.FirstName,
                LastName = customer.LastName,
                userId = customer.Id,
                PhoneNumber = customer.PhoneNumber,
            };
            return myModel;
        }
        [HttpGet]
        public IQueryable<SelectListItem> get_al_lRoles()
        {
            return RoleManager.Roles
                .Where(i => i.Name != "Admin")
              .Select(i => new SelectListItem(i.Name, i.Name));
        }
        public async Task<Microsoft.AspNetCore.Identity.SignInResult> SignInCustomer(LoginModel model)
        {
            return await SignInManager.PasswordSignInAsync
                        (model.UserName, model.Password, model.RememberMe,
                             true);
        }
        public async void SignOutCustomer()
        {
            await SignInManager.SignOutAsync();
        }
        public async Task<ResultViewModel> getToken(LoginModel model)
        {
            var user = await UserManager.FindByNameAsync(model.UserName);
            if (user == null)
            {
                ResultViewModel Notfound = new ResultViewModel()
                {
                    Success = false,
                    Message = "User Not Found",
                };
                return Notfound;
            }
            List<Claim> claims = new List<Claim>();

            var roles = await UserManager.GetRolesAsync(user);
            roles.ToList().ForEach(i =>
            {
                claims.Add(new Claim(ClaimTypes.Role, i));
            });

            claims.Add(new Claim(ClaimTypes.Name, model.UserName));
            claims.Add(new Claim(ClaimTypes.NameIdentifier, user.Id));
            claims.Add(new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()));

            JwtSecurityToken token
                = new JwtSecurityToken
                (
                    signingCredentials:
                     new SigningCredentials
                     (
                         new SymmetricSecurityKey(Encoding.ASCII.GetBytes("IOLJYHSDSIoleJHsdsdsas98WeWsdsdQweweHgsgdf_&6#2"))
                         ,
                         SecurityAlgorithms.HmacSha256
                     ),
                    expires: DateTime.Now.AddDays(5),
                    claims: claims
                );

            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            ResultViewModel myModel = new ResultViewModel()
            {
                Success = true,
                Message = "Successfulyy Loged In",
                email = user.Email,
                expiration = token.ValidTo,
                token = tokenValue,
                roles = roles.ToList(),
                username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                userId = user.Id,
              PhoneNumber = user.PhoneNumber,
        };

            return myModel;
        }
        public async Task<ResultViewModel> getToken(Customer user)
        {
            List<Claim> claims = new List<Claim>();
            var roles = await UserManager.GetRolesAsync(user);

            roles.ToList().ForEach(i =>
            {
                claims.Add(new Claim(ClaimTypes.Role, i));
            });


            JwtSecurityToken token
                = new JwtSecurityToken
                (
                    signingCredentials:
                     new SigningCredentials
                     (
                         new SymmetricSecurityKey(Encoding.ASCII.GetBytes("IOLJYHSDSIoleJHsdsdsas98WeWsdsdQweweHgsgdf_&6#2"))
                         ,
                         SecurityAlgorithms.HmacSha256
                     ),
                    expires: DateTime.Now.AddDays(5),
                    claims: claims
                );


            string tokenValue = new JwtSecurityTokenHandler().WriteToken(token);
            ResultViewModel myModel = new ResultViewModel()
            {
                Success = true,
                Message = "Successfulyy Loged In",
                email = user.Email,
                expiration = token.ValidTo,
                token = tokenValue,
                roles = roles.ToList(),
                username = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                userId = user.Id,
                PhoneNumber = user.PhoneNumber,
            };

            return myModel;

        }
        public Customer getuser(String id)
        {
            return mycontext.Customers
                 .FirstOrDefault(i => i.Id == id);
        }
        public ResultViewModel getuserbyusername(string name)
        {
            Customer user = mycontext.Customers
                .FirstOrDefault(i => i.UserName == name);
            ResultViewModel myModel = new ResultViewModel();
            if (user != null)
            {
                myModel = new ResultViewModel()
                {
                    Success = true,
                    Message = "Successfuly Get Data",
                    email = user.Email,
                    username = user.UserName,
                    FirstName = user.FirstName,
                    LastName = user.LastName,
                    userId = user.Id,
                    PhoneNumber = user.PhoneNumber,
                };
            }
            else
            {
                myModel = new ResultViewModel()
                {
                    Success = false,
                    Message = "Successfulyy not found Data",
                };
            }

            return myModel;
        }
    }

}
