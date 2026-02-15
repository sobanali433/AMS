using AMS.Data;
using AMS.Models;

namespace AMS.Repository
{
    public interface IStockRepository
    {
        //Task<List<Stock>> GetStocksByBranchAsync(int branchId);
        List<StockModel> GetStockList(int? branchId);

    }
}
