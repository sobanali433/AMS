using AMS.Data;

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
        public List<Product> GetList()
        {
            return _context.Products.ToList();
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


    }
}
