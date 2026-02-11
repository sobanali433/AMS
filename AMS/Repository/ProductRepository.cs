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



    }
}
