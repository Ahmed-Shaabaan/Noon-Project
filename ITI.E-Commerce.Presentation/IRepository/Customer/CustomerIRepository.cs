using ITI.E_Commerce.Models;
using ITI.E_Commerce.Presentation.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.Security.Claims;
using X.PagedList;

namespace ITI.E_Commerce.Presentation.IRepository
{
    public interface CustomerIRepository
    {
        public Task<IdentityResult> CreateCustomer(UserCreateModel obj);
        public IQueryable<SelectListItem> get_al_lRoles();
        public IQueryable<SelectListItem> get_al_lRole();
        public Task<Microsoft.AspNetCore.Identity.SignInResult> SignInCustomer(LoginModel model);
        public  void SignOutCustomer();
        public Task<IdentityResult> ConfirmEmailCustomer(string uid, string token);
        public void DeleteCustomer(string id);
        public Task<IEnumerable<Customer>> GetAllInterFaceCustomers();
        public Task<IEnumerable<Customer>> GetAllAdmins();
        public Task<IEnumerable<Customer>> GetAllShippers();
        public Task<Customer> UpdateInterFAceCustomer(ClaimsPrincipal User);
        public void UpdateCustomer(UserUpdateModel obj, Customer customer);
        public Task<List<string>> GetAllRoleUser(Customer User);
        public Task<Boolean> UpdateRoleUser(Customer user, RoleUpdateModel model);
        public  Task<IdentityResult> ChangePassword(ChangePasswordModel obj, ClaimsPrincipal User);
        public Customer GetCustomerId(String Id);
        public Task<bool> CheckRole(LoginModel model);
    }
}
