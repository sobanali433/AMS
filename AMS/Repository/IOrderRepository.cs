using AMS.Data;

namespace AMS.Repository
{
    public interface IOrderRepository
    {
        List<Order> GetAllWithDetailsAsync();
    }
}
