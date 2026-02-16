using AMS.Data;

namespace AMS.Repository
{
    public class OrderRepository : IOrderRepository
    {
        private readonly AmsContext _context;
        public OrderRepository(AmsContext context)
        {
            _context = context;
        }
        public List<Order> GetAllWithDetailsAsync()
        {
            return _context.Orders
                //.Include(o => o.Products)
                //.Include(o => o.BranchMasters)
                .ToList();
        }
    }
}
