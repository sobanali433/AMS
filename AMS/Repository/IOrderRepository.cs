using AMS.Data;

namespace AMS.Repository
{
    public interface IOrderRepository
    {
        Task<Order> AddOrderAsync(Order order);
        List<Order> GetAllWithDetailsAsync();
    }
}
