using ITI.E_Commerce.Models;
using ITI.E_Commerce.Presentation.Models;

namespace ITI.E_Commerce.Presentation.IRepository.BrandR
{
    public class BrandRepository : IBrandRepository
    {
        private readonly MyContext context;
        public BrandRepository(MyContext _context)
        {
            context = _context;
        }
        public void Delete(int id)
        {

            var brand = context.Brands.FirstOrDefault(i => i.Id == id);
            context.Brands.Remove(brand);
            context.SaveChanges();
        }

        public List<Brand> GetAll()
        {
            var brands= context.Brands.ToList();
            return brands;
        }

        public Brand GetById(int id)
        {
            var brand = context.Brands.FirstOrDefault(p => p.Id == id);
            return brand;
        }

        public void Insert(BrandCreateModel model)
        {
            IFormFile file = model.Image as IFormFile;
            string BinaryPath = Guid.NewGuid().ToString() + file.FileName;
            string image = BinaryPath;

            FileStream fs = new FileStream(
              Path.Combine(Directory.GetCurrentDirectory(),
              "wwwroot", "BrandImages", BinaryPath)
              , FileMode.OpenOrCreate, FileAccess.ReadWrite);

            file.CopyTo(fs);
            fs.Position = 0;


            var Brand = new Brand()
            {
                Name = model.Name,
                Image = image,
            };
            context.Add(Brand);
            context.SaveChanges();
        }

        public void Save()
        {
            context.SaveChanges();
        }

        public void Update(BrandCreateModel model, Brand brand)
        {
            brand.Name = model.Name;
            context.Update(brand);
            context.SaveChanges();
        }



    }
}
