using AMS.Data;
using AMS.Models;

namespace AMS.Repository
{
    public interface IStockRepository
    {
        Task<Stock> GetByProductAndBranchAsync(int productId, int branchId);
        Task<List<Stock>> GetStockListAsync(int branchId);
        Task<List<Product>> GetProductAsync();
        IEnumerable<BranchMaster> GetBranches();
        IEnumerable<Product> GetProducts();

        Task AddAsync(Stock stock);
        Task UpdateAsync(Stock stock);
        //Task GetById(UserMaster user);
        



    }
}
