using ITI.E_Commerce.Models;
using ITI.E_Commerce.Presentation.Models;
using ITI.E_Commerce.Presentation.Models.CategoryModel;

namespace ITI.E_Commerce.Presentation.IRepository.BrandR
{
    public interface IBrandRepository
    {
        public List<Brand> GetAll();
        public Brand GetById(int id);
        void Insert(BrandCreateModel brand);
        void Update(BrandCreateModel model, Brand brand);
        public void Delete(int id);
        public void Save();
    }
}
