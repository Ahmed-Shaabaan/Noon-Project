using System;
using ITI.E_Commerce.Models;

namespace ITI.E_Commerce.Api.Specifications
{
	public class ProductCategoryWithFilterCount:BaseSpecification<Category>
    {
        public ProductCategoryWithFilterCount(ProductFilterParam productParams) :
            base(x =>
                (string.IsNullOrEmpty(productParams.Search) || x.Name.ToLower().Contains(productParams.Search))
                )
        {

        }
    }
}

