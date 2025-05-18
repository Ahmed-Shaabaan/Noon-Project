using System;
using ITI.E_Commerce.Models;
using ITI.E_Commerce.Presentation.Models;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using X.PagedList;

namespace ITI.E_Commerce.Presentation.IRepository
{
	public class ProductRepository: IProductRepository
    {

		private readonly MyContext context;
        private readonly IWebHostEnvironment Webhost;
        private readonly IConfiguration configuration;
        public ProductRepository(IWebHostEnvironment _webhost, MyContext _context, IConfiguration _configuration)
		{
			context = _context;
			Webhost = _webhost;
            configuration = _configuration;
        }

        public async Task<Product>CreateNew(ProductCreateModel entity)
        {
            Product newProduct = new Product();
            if (entity.NewPrece != 0)
            {
                int Sail = CalculateSail(entity.UnitPrice, entity.NewPrece);

                PriceOffer priceOffer = new PriceOffer()
                {
                   NewPrice = entity.NewPrece,
                    PromotionalText = $"Save {Sail}"
                };
                 newProduct = new Product()
                {
                    Name = entity.Name,
                    Description = entity.Description,
                    UnitPrice = entity.UnitPrice,
                    Color = entity.Color,
                    Size = entity.Size,
                    BrandID = entity.BrandID,
                    CategoryID = entity.CategoryID,
                    PriceOffer = priceOffer,
                    Quantity = entity.Quantity,
                    ProductImages = SetProductImages(entity.Images),
                    Discount = Sail,

                };

            }
            else
            {

                 newProduct = new Product()
                {
                    Name = entity.Name,
                    Description = entity.Description,
                    UnitPrice = entity.UnitPrice,
                    Color = entity.Color,
                    Size = entity.Size,
                    BrandID = entity.BrandID,
                    CategoryID = entity.CategoryID,
                    Quantity = entity.Quantity,
                    ProductImages = SetProductImages(entity.Images),
                 

                };

            }




            await context.Products.AddAsync(newProduct);
             context.SaveChanges();

            return newProduct ;
        }

        public void Delete(int id)
        {
            var Product = context.Products.FirstOrDefault(p => p.Id == id);
            context.Products.Remove(Product);
            context.SaveChanges();
        }

        public Task<Product> Get(int id)
        {
            var Product = context.Products.FirstOrDefaultAsync(p=> p.Id == id);
            return Product;
        }

        public async Task<IPagedList<Product>> GetAll(int pageIndex = 1, int pageSize = 2)
        {
            var products = await context.Products.ToPagedListAsync(pageIndex, pageSize);
            return products;
        }

        public  List<Product> GetAllProduct()
        {
            var products =  context.Products.ToList();
            return products;
        }


        public void Update(Product product, int id)
        {
            Product pro = context.Products.FirstOrDefault(i => i.Id == id);
            pro.Name = product.Name;
            pro.Description = product.Description;
            pro.UnitPrice = product.UnitPrice;
            pro.Quantity = product.Quantity;
            pro.Discount = product.Discount;
            pro.CategoryID = product.CategoryID;
            pro.BrandID = product.BrandID;
            context.Products.Update(pro);
            context.SaveChanges();


        }

        public  List<ProductImages> SetProductImages(List<IFormFile> Imgee)
        {
            List<ProductImages> images = new List<ProductImages>();

            foreach (IFormFile file in Imgee)
            {
                string BinaryPath = Guid.NewGuid().ToString() + file.FileName;
                images.Add(new ProductImages
                {
                    Path = BinaryPath,
                });
                FileStream fs = new FileStream(
                  Path.Combine(Directory.GetCurrentDirectory(),
                  "wwwroot", "ProductImages", BinaryPath)
                  , FileMode.OpenOrCreate, FileAccess.ReadWrite);
                file.CopyTo(fs);
                fs.Position = 0;
                fs.Close();

            }
            return images;
        }

        public int CalculateSail(int Price, int newPrice)
        {
            return Price - newPrice;
        }

        public IFormFile getImageFromFile(string path)
        {
            using var stream = new MemoryStream(System.IO.File.ReadAllBytes(path).ToArray());
            var formFile = new FormFile(stream, 0, stream.Length, "streamFile", path.Split(@"\").Last());
            return formFile;
        }
        public void RemoveImage(string imgPath)
        {
            ///remove the url
            //string  words = imgPath.Split("ProductImages/");
            //imgPath = words;

            string image = Path.Combine(Webhost.WebRootPath, "ProductImages", imgPath);
            FileInfo fi = new FileInfo(image);
            System.IO.File.Delete(image);
            fi.Delete();

        }

        public  IQueryable<SelectListItem> GetAllBrand()
        {
             var brands = context.Brands.Select(i => new SelectListItem(i.Name, i.Id.ToString())).AsNoTracking();
            return brands;
        }

        public IQueryable<SelectListItem> GetAllCategory()
        {
            var Categories= context.Categories.Select(i => new SelectListItem(i.Name, i.Id.ToString())).AsNoTracking();
            return Categories;
        }

     
    }
}

