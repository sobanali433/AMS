using AMS.Data;

namespace AMS.Repository
{
    public interface IBranchRepository
    {
        List<BranchMaster> GetAllAsync();
        BranchMaster GetById(int id);
        Task<bool> AddBranchAsync(BranchMaster model);
        Task<bool> UpdateUserAsync(BranchMaster model);
        (bool isSuccess, string message) Delete(int id);

    }
}
