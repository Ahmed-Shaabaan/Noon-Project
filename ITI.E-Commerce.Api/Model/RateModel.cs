using System;
namespace ITI.E_Commerce.Api.Model
{
	public class RateModel
	{
        public int ProductID { get; set; }
        public int NumberOfStart { get; set; }
        public string Comment { get; set; }
        public DateTime Date { get; set; } = DateTime.Now;
        public string CustomerName { get; set; }


    }
}

