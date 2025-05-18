using System;
using ITI.E_Commerce.Models;
using ITI.E_Commerce.Presentation.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using X.PagedList;

namespace ITI.E_Commerce.Presentation.IRepository
{
	public interface IProductRepository
    {
		public Task<Product> CreateNew(ProductCreateModel obj);
		//public Product Update(ProductCreateModel entity);
		public void Update(Product product, int id);
		public void Delete(int id);
		public Task<IPagedList<Product>> GetAll(int pageIndex = 1, int pageSize = 2);
		public Task<Product> Get(int id);
		public IQueryable<SelectListItem> GetAllBrand();
		public IQueryable<SelectListItem> GetAllCategory();
		public  List<Product> GetAllProduct();




    }
}

