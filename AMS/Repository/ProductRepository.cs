using AMS.Data;
using AMS.Enums;
using Microsoft.EntityFrameworkCore;

namespace AMS.Repository
{
    public class ProductRepository : IProductRepository
    {
        private readonly AmsContext _context;

        public ProductRepository(AmsContext amsContext)
        {
            _context = amsContext;
        }
        //public List<Product> GetAll()
        //{
        //    return _context.Products.ToList();
        //}
        //public List<Product> ()
        //{
        //    return _context.Products
        //  .Include(p => p.Categories) // Load related Category
        //  .Select(p => new
        //  {
        //      p.ProductId,
        //      p.ProductName,
        //      p.SKU,
        //      p.Price,
        //        p.Categories.CategoryName, // 👈 yahan ID nahi, name chahiye
        //      p.IsActive
        //  })
        //  .ToList<object>();
        //    //return _context.Products.Include(p => p).ToList();
        //    //return _context.Products.ToList();
        //}
        public List<object> GetProductList()
        {
            return _context.Products
                .Include(p => p.Categories).AsEnumerable()  
                .Select(p => new
                {
                    p.ProductId,
                    p.ProductName,
                    p.SKU,
                    p.Price,
                    CategoryName = p.Categories.CategoryName, 
                    //ProductType = p.ProductType.ToString(),    
                    p.IsActive
                })
                .ToList<object>();
        }


        public async Task<bool> AddUserAsync(UserMaster user)
        {
            await _context.UserMasters.AddAsync(user);
            return await _context.SaveChangesAsync() > 0;
        }
        public List<Category> GetCategories()
        {
            return _context.Categories.ToList();
        }


        public async Task<Product?> GetByIdAsync(int id)
        {
            return await _context.Products.FindAsync(id);
        }

        public async Task<bool> AddproductAsync(Product product)
        {
            await _context.Products.AddAsync(product);
            return await _context.SaveChangesAsync() > 0;
        }
        public async Task<bool> UpdateAsync(Product product)
        {
            _context.Products.Update(product);
            return await _context.SaveChangesAsync() > 0;
            

        }
        public (bool isSuccess, string message) ProductDelete(int id)
        {
            var product = _context.Products.Find(id);
            if (product == null)
                return (false, "product not found.");

            product.IsActive = !product.IsActive;

            _context.Products.Update(product);
            _context.SaveChanges();

            string message = product.IsActive ? "Product activated successfully." : "Product  de-activated successfully.";

            return (true, message);
        }


    }
}
