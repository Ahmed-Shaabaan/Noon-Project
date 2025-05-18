using NuGet.Common;

namespace ITI.E_Commerce.Api.Model
{
    public class ResultViewModel
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public string token { get; set; }
        public string userId { get; set;}
        public string username { get; set;}
        public string email { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime expiration { get; set;}
        public List<string> roles { get; set; }
        public Object Data { get; set; }
        public string PhoneNumber { get; set; }
    }
}
