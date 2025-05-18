using ITI.E_Commerce.Api.Model;
using ITI.E_Commerce.Models;
using ITI.E_Commerce.Presentation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;

namespace ITI.E_Commerce.Api.IRepository
{
    public interface ICustomerRepository
    {
        public Task<IdentityResult> CreateCustomer(UserCreateModel obj);
        public ResultViewModel UpdateCustomer(UserUpdateModel obj, Customer customer);
        public IQueryable<SelectListItem> get_al_lRoles();
        public Task<Microsoft.AspNetCore.Identity.SignInResult> SignInCustomer(LoginModel model);
        public Task<IdentityResult> ChangePassword(ChangePasswordModel obj, Customer User);
        public void SignOutCustomer();
        public Task<ResultViewModel> getToken(LoginModel model);
        public Customer getuser(String id);
        public Task<ResultViewModel> getToken(Customer model);
        public ResultViewModel getuserbyusername(string name);
    }
}
