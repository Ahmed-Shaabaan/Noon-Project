using Castle.Core.Resource;
using ITI.E_Commerce.Models;
using ITI.E_Commerce.Presentation.Models.CategoryModel;
using Microsoft.EntityFrameworkCore;

namespace ITI.E_Commerce.Presentation.IRepository
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly MyContext context;
        public CategoryRepository(MyContext _context)
        {
            context = _context;
        }

        public void Delete(int id)
        {
            var category = context.Categories.FirstOrDefault(i => i.Id == id);
            context.Categories.Remove(category);
            context.SaveChanges();
        }

        public List<Category> GetAll()
        {
            var categories = context.Categories.ToList();
            return categories;
        }

        public Category GetById(int id)
        {
            var category = context.Categories.FirstOrDefault(p => p.Id == id);
            return category;
        }

        public void Insert(CategoryModel model)
        {

            IFormFile file = model.Image as IFormFile;
            string BinaryPath = Guid.NewGuid().ToString() + file.FileName;
            string image = BinaryPath;

            FileStream fs = new FileStream(
              Path.Combine(Directory.GetCurrentDirectory(),
              "wwwroot", "CategoryImage", BinaryPath)
              , FileMode.OpenOrCreate, FileAccess.ReadWrite);

            file.CopyTo(fs);
            fs.Position = 0;

            var Category = new Category() {
            Name = model.Name,
            Image = image,
            };
            context.Add(Category);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(CategoryModel model,Category category)
        {
            category.Name = model.Name;
            context.Update(category);
            context.SaveChanges();
            //context.Update(category);
        }
    }
}
