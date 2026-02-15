using AMS.Data;

namespace AMS.Repository
{
    public class BranchRepository : IBranchRepository
    {
        private readonly AmsContext _context;
        public BranchRepository(AmsContext context)
        {
            _context = context;
        }
        public List<BranchMaster> GetAllAsync()
        {
            return _context.BranchMasters.ToList();
        }
    }
}
