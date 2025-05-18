using ITI.E_Commerce.Models;
using ITI.E_Commerce.Presentation.Models.CategoryModel;

namespace ITI.E_Commerce.Presentation.IRepository
{
    public interface ICategoryRepository
    {
        public List<Category> GetAll();
        public Category GetById(int id);
        void Insert(CategoryModel category);
        void Update(CategoryModel model, Category category);
        public void Delete(int id);
        public void Save();
    }
}
