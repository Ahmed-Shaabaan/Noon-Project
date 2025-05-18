using ITI.E_Commerce.Api.Model;
using ITI.E_Commerce.Models;

using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ITI.E_Commerce.Presentation.Models;
using ITI.E_Commerce.Api.IRepository;
using System.IdentityModel.Tokens.Jwt;
using System.Text;
using Microsoft.IdentityModel.Tokens;
using System.Security.Claims;
using ITI.E_Commerce.Api.Error;
using Castle.Core.Resource; 

namespace ITI.E_Commerce.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]/[action]")]
    public class CustomerController : ControllerBase
    {
        private readonly UserManager<Customer> _userManager;
        private readonly IConfiguration _config;
        private readonly ICustomerRepository productRepository;
        public CustomerController(ICustomerRepository _productRepository, IConfiguration config, UserManager<Customer> userManager)
        {
            productRepository = _productRepository;
            _config = config;
            _userManager = userManager;
        }
        [HttpPost]
        public async Task<GetData> SignUp([FromBody] UserCreateModel model)
        {
            GetData response = new GetData();
            if (ModelState.IsValid == false)
            {
                response.data = ModelState;
                response.Success = false;
                response.Message = " Error";
                return response;
            }
               
            else
            {

                IdentityResult result = await productRepository.CreateCustomer(model);
                if (result.Succeeded == false)
                {
                    result.Errors.ToList().ForEach(i => { ModelState.AddModelError("", i.Description); });
                    response.data = result.Errors.ToList();
                    response.Success = false;
                    response.Message = " Error";
                    return response;
                }
                else
                {

                    //string token = await UserManager.GenerateEmailConfirmationTokenAsync(user);
                    //var client = new SmtpClient("smtp.mailtrap.io", 2525)
                    //{
                    //    Credentials = new NetworkCredential("2776941d240720", "519e53aa9648ab"),
                    //    EnableSsl = true
                    //};
                    //string body = $"Please Click Here For Verification: https://localhost:59750/User/ConfirmEmail?uid={user.Id}&token={token}";
                    //client.Send("Info@lib.edu.com", model.Email, "Email Confirmation", body);
                    response.data = null;
                    response.Success = true;
                    response.Message = " User Register";
                    return response;
                }
            }
        }

        [HttpPost]
        public async Task<ResultViewModel> SignIn([FromBody] LoginModel model)
        {
            ResultViewModel myModel = new ResultViewModel();
            if (ModelState.IsValid == false)
            {
                myModel.Success = false;
            }
            else
            {
                var result
                     = await productRepository.SignInCustomer(model);
                if (result.Succeeded != true)
                {
                    myModel.Success = false;
                    myModel.Message = "Invalid UserName Or Password ";
                }
                else if (result.IsLockedOut == true)
                {
                    myModel.Success = false;
                    myModel.Message = "Is Locked Out";
                }
                else if (result.IsNotAllowed == true)
                {
                    myModel.Success = false;
                    myModel.Message = "Is Not Allowed";
                }
                else if (result.Succeeded == true)
                {
                    myModel = await productRepository.getToken(model);
                }
            }
            return myModel;
        }



        //[Authorize(AuthenticationSchemes = "Bearer")]
        //[HttpGet ]
        //public async Task<IActionResult> GetCurrantUser()
        //{
        //    ResultViewModel myModel = new ResultViewModel();
        //var tokenHandler = new JwtSecurityTokenHandler();
        //var SecretKey = _config.GetValue<string>("JWT:SecretKey");
        //var key = Encoding.ASCII.GetBytes(SecretKey);
        //var token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
        //tokenHandler.ValidateToken(token, new TokenValidationParameters
        //{
        //    ValidateIssuerSigningKey = true,
        //    IssuerSigningKey = new SymmetricSecurityKey(key),
        //    ValidateIssuer = false,
        //    ValidateAudience = false,
        //    ClockSkew = TimeSpan.Zero
        //}, out SecurityToken validatedToken);

        [Authorize(AuthenticationSchemes = "Bearer")]
        [HttpGet]
        public async Task<IActionResult> GetCurrantUser()
        {
            ResultViewModel myModel = new ResultViewModel();
            var tokenHandler = new JwtSecurityTokenHandler();
            var SecretKey = _config.GetValue<string>("JWT:SecretKey");
            var key = Encoding.ASCII.GetBytes(SecretKey);
            var token = HttpContext.Request.Headers["Authorization"].ToString().Split(' ')[1];
            tokenHandler.ValidateToken(token, new TokenValidationParameters
            {
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(key),
                ValidateIssuer = false,
                ValidateAudience = false,

            }, out SecurityToken validatedToken);


            var jwtToken = (JwtSecurityToken)validatedToken;
            var userId = jwtToken.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
            var user = await _userManager.FindByIdAsync(userId);

            if (user != null)
            {
                myModel = await productRepository.getToken(user);
                myModel.token = token;
                return Ok(new
                {
                    Model = myModel
                });
            }
            else
            {
                return Unauthorized(new ApiResponse(401));
            }
            //}


        }


        [Authorize]
        [HttpGet(Name = "SignOut")]
        public new async Task<bool> SignOut()
        {
            productRepository.SignOutCustomer();
            return true;
        }

        [HttpPost]
        [Authorize]
        public async Task<GetData> ChangePassword( ChangePasswordModel model, string id)
        {
            GetData response = new GetData();
            Customer customer = productRepository.getuser(id);
            if (ModelState.IsValid == false) {
                response.Success = false;
                response.Message = "Password not Change";
                response.data = ModelState;
                return response;
            }
            else
            {
                var c = await productRepository.ChangePassword(model, customer);
                if (c.Succeeded)
                {
                    response.Success = true;
                    response.Message = "Password Change";
                    response.data = null;
                    return response;
                }
                else
                {
                    c.Errors.ToList().ForEach(i => { ModelState.AddModelError("", i.Description); });
                    response.Success = false;
                    response.Message = "Password not Change";
                    response.data = c.Errors.ToList();
                    return response;
                }
            }
        }
        [HttpGet]
        [Authorize]
        public ResultViewModel getuserByusername(string username)
        {

            return productRepository.getuserbyusername(username);

        }

        [HttpPost]
        [Authorize]
        public ResultViewModel UpdateCustomer(string id, UserUpdateModel model)
        {
            ResultViewModel response = new ResultViewModel();
            if (ModelState.IsValid == false)
            {
                response.Data = ModelState;
                response.Success = false;
                response.Message = " Error";
                return response;
            }
            else
            {
                var customer = productRepository.getuser(id);
                if (customer == null)
                {
                    response.Data = null;
                    response.Success = false;
                    response.Message = "Not Found User";
                    return response;
                }
               
                response = productRepository.UpdateCustomer(model, customer);
               
                return response;
            }
        }
    }
}
