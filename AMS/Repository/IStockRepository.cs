using AMS.Data;
using AMS.Models;

namespace AMS.Repository
{
    public interface IStockRepository
    {
        Task<Stock> GetByProductAndBranchAsync(int productId, int branchId);
        Task<List<Stock>> GetStockListAsync(int branchId);

        Task AddAsync(Stock stock);
        //Task AddAsync(Order order);
        Task UpdateAsync(Stock stock);

    }
}
