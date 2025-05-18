using System;
using ITI.E_Commerce.Models;

namespace ITI.E_Commerce.Api.Specifications
{
	public class ProductCategorySpecification:BaseSpecification<Category>
	{
        public ProductCategorySpecification(ProductFilterParam productParams) : base(x =>
        (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search)) )
        {
            AddOrderBy(p => p.Name);

            var skip = productParams.PageSize * (productParams.PageIndex - 1);
            var take = productParams.PageSize;
            ApplyPaging(skip, take);
        }

        public ProductCategorySpecification(int id) : base(x => x.Id == id)
        {

        }
    }
}

