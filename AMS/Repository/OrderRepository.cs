using AMS.Data;
using Microsoft.EntityFrameworkCore;

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
                .Include(o => o.BranchMasters)
                .Include(o => o.Products)
                .ToList();
        }

        public async Task<Order> AddOrderAsync(Order order)
        {
            await _context.Orders.AddAsync(order);
            await _context.SaveChangesAsync();
            return order;
        }



    }
}
