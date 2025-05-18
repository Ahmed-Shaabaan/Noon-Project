using System;
using ITI.E_Commerce.Models;

namespace ITI.E_Commerce.Api.Specifications
{
	public class ProductBrandSpecification :BaseSpecification<Brand>
	{
        public ProductBrandSpecification(ProductFilterParam productParams) : base(x =>
           (string.IsNullOrEmpty(productParams.Search)|| x.Name.ToLower().Contains(productParams.Search)  )
           )
        {
            AddOrderBy(p => p.Name);

            var skip = productParams.PageSize * (productParams.PageIndex - 1);
            var take = productParams.PageSize;
            ApplyPaging(skip, take);
        }

        public ProductBrandSpecification(int id) : base(x => x.Id == id)
        {

        }
    }
}

