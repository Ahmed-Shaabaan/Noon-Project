using System;
using ITI.E_Commerce.Models;

namespace ITI.E_Commerce.Api.Specifications
{
	public class ProductsWithCategoryAndBrandsSpecification : BaseSpecification<Product>	
    {
		public ProductsWithCategoryAndBrandsSpecification(ProductFilterParam productParams) :base(x=>
          (string.IsNullOrEmpty(productParams.Search) || (x.Name.ToLower().Contains(productParams.Search) || x.Brand.Name.ToLower().Contains(productParams.Search) || x.Brand.Name.ToLower().Contains(productParams.Search)|| x.Description.ToLower().Contains(productParams.Search)))
        &&
            (!productParams.BrandId.HasValue || x.BrandID == productParams.BrandId) &&
            (!productParams.TypeId.HasValue || x.CategoryID == productParams.TypeId) &&
            (!productParams.PriceFrom.HasValue || x.UnitPrice >= productParams.PriceFrom) &&
            (!productParams.PriceTo.HasValue || x.UnitPrice <= productParams.PriceTo) &&
            (string.IsNullOrEmpty(productParams.Color) || x.Color == productParams.Color))
        {

            AddInclude(x => x.Brand);
            AddInclude(x => x.Category);
            AddInclude(p => p.ProductImages);
            AddInclude(p => p.PriceOffer);
            AddOrderBy(x => x.Name);
            ApplyPaging(productParams.PageSize * (productParams.PageIndex - 1), productParams.PageSize);

            if (!string.IsNullOrEmpty(productParams.Sort))
            {
                switch (productParams.Sort)
                {
                    case "priceAsc":
                        AddOrderBy(p => p.UnitPrice);
                        break;
                    case "priceDesc":
                        AddOrderByDescending(p => p.UnitPrice);
                        break;
                    case "nameDesc":
                        AddOrderByDescending(p => p.Name);
                        break;
                    case "nameAsc":
                        AddOrderBy(p => p.Name);
                        break;
                    default:
                        AddOrderBy(n => n.Name);
                        break;
                }
            }
        }
        public ProductsWithCategoryAndBrandsSpecification(int id) : base(x => x.Id == id)
        {

            AddInclude(x => x.Brand);
            AddInclude(x => x.Category);
            AddInclude(p => p.ProductImages);
            AddInclude(p => p.PriceOffer);
        }
        public ProductsWithCategoryAndBrandsSpecification() : base()
        {

            AddInclude(x => x.Brand);
            AddInclude(x => x.Category);
            AddInclude(p => p.ProductImages);
            AddInclude(p => p.PriceOffer);
        }
    }
}


