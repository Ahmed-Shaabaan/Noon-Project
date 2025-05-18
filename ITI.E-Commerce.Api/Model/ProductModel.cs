using System;
using ITI.E_Commerce.Models;

namespace ITI.E_Commerce.Api.Model
{
	public class ProductModel
	{
        public int Id { get; set; }

        public string Name { get; set; }

        public decimal UnitPrice { get; set; }

        public int Quantity { get; set; }

        public string Description { get; set; }

        public double Discount { get; set; }

        public string Color { get; set; }

        public string Size { get; set; }

        public int CategoryID { get; set; }

        public int BrandID { get; set; }

        public double NewPrice { get; set; }

        public int Sail { get; set; }

        public string PriceOffer { get; set; }

        public string  Category { get; set; }

        public  List<string> ProductImages { get; set; }

        public string  Brand { get; set; }



    }
}

