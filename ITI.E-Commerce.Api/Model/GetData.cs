using Microsoft.AspNetCore.Mvc;

namespace ITI.E_Commerce.Api.Model
{
    public class GetData 
    {
        public bool Success { get; set; }
        public string Message { get; set; }
        public object data { get; set; }
    }
}
