using System;
using ITI.E_Commerce.Models;

namespace ITI.E_Commerce.Api.Specifications
{
	public class ProductsWithFiltersCount:BaseSpecification<Product>
    {
        public ProductsWithFiltersCount(ProductFilterParam productParams) :
            base(x =>
                  (string.IsNullOrEmpty(productParams.Search) || (x.Name.ToLower().Contains(productParams.Search) || x.Brand.Name.ToLower().Contains(productParams.Search) || x.Brand.Name.ToLower().Contains(productParams.Search))) &&
            (!productParams.BrandId.HasValue || x.BrandID == productParams.BrandId) &&
            (!productParams.TypeId.HasValue || x.CategoryID == productParams.TypeId) &&
            (!productParams.PriceFrom.HasValue || x.UnitPrice >= productParams.PriceFrom) &&
            (!productParams.PriceTo.HasValue || x.UnitPrice <= productParams.PriceTo) &&
            (string.IsNullOrEmpty(productParams.Color) || x.Color == productParams.Color))
        {


        }
    }
}

