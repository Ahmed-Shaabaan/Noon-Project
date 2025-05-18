using System;
namespace ITI.E_Commerce.Api.Model
{
	public class AddRateModel
	{
        public int productId { get; set; }

        public string Comment { get; set; }

        public int stars { get; set; }
    }
}

