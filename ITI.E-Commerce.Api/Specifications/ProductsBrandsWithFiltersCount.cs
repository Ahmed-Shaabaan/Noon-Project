using System;
using ITI.E_Commerce.Models;

namespace ITI.E_Commerce.Api.Specifications
{
    public class ProductsBrandsWithFiltersCount : BaseSpecification<Brand>
    {
        public ProductsBrandsWithFiltersCount(ProductFilterParam productBrandsParams) :
           base(x =>
               (string.IsNullOrEmpty(productBrandsParams.Search) || x.Name.ToLower().Contains(productBrandsParams.Search))
               )
        {

        }
    }
}

