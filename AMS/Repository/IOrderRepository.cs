using AMS.Data;

namespace AMS.Repository
{
    public interface IOrderRepository
    {
        Task<Order> AddOrderAsync(Order order);
        //List<Order> GetAllWithDetailsAsync();
        Task<List<Order>> GetOrdersByRoleAsync(string role, int branchId);

    }
}
