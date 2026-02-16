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
      
        public BranchMaster GetById(int id)
        {
            //return _context.BranchMasters.FirstOrDefault(x=>x.BranchId == id);
            return _context.BranchMasters.Find(id);
        }
        public async Task<bool> AddBranchAsync(BranchMaster model)
        {
            await _context.BranchMasters.AddAsync(model);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUserAsync(BranchMaster model)
        {
             _context.BranchMasters.Update(model);
            return await _context.SaveChangesAsync()>0;
        }

        public (bool isSuccess, string message) Delete(int id)
        {
            var user = _context.BranchMasters.Find(id);
            if (user == null)
                return (false, "User record not found.");

            user.IsActive = !user.IsActive;

            _context.BranchMasters.Update(user);
            _context.SaveChanges();

            string message = user.IsActive ? "Branch activated successfully." : "Branch de-activated successfully.";

            return (true, message);
        }
    }
}
