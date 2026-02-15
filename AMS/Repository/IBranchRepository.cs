using AMS.Data;

namespace AMS.Repository
{
    public interface IBranchRepository
    {
        List<BranchMaster> GetAllAsync();

    }
}
