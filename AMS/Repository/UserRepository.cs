using AMS.Data;
using AMS.Models;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;

namespace AMS.Repository
{
    public class UserRepository : IUserRepository
    {
        private readonly AmsContext _context;
        public UserRepository(AmsContext context)
        {
            _context = context;
        }

        //public async Task<List<UserMaster>> GetAllUsersWithoutSuperAdmin()
        //{
        //    return await _context.UserMasters
        //        .Include(u => u.Role)
        //        .Where(u => u.Role != null && u.Role.RoleName != "SuperAdmin")
        //        .ToListAsync();
        //}
      
        public UserMaster GetById(int id)
        {
            return _context.UserMasters.Include(u => u.Roles).Include(u => u.BranchMasters).FirstOrDefault(x => x.UserMasterId == id);

            //return _context.UserMasters.Include(b => b.BranchMasters.BranchId).Include(r => r.Roles.RoleId).FirstOrDefault(u => u.UserMasterId == id);
        }
        public IEnumerable<Role> GetRoles()
        {
            return _context.Roles.AsNoTracking().Where(u => u.RoleName != "SuperAdmin").ToList();
        }
        public IEnumerable<BranchMaster> GetBranches()
        {
            return _context.BranchMasters.ToList();
        }
        public async Task<bool> AddUserAsync(UserMaster user)
        {
            await _context.UserMasters.AddAsync(user);
            return await _context.SaveChangesAsync() > 0;
        }

        public async Task<bool> UpdateUserAsync(UserMaster user)
        {
            _context.UserMasters.Update(user);
            return await _context.SaveChangesAsync() > 0;
        }
        

        public async Task<UserMaster?> GetByIdAsync(int id)
        {
            return await _context.UserMasters.FindAsync(id);
        }

        


        public (bool isSuccess, string message) Delete(int id)
        {
            var user = _context.UserMasters.Find(id);
            if (user == null)
                return (false, "User record not found.");

            user.IsActive = !user.IsActive;

            _context.UserMasters.Update(user);
            _context.SaveChanges();

            string message = user.IsActive ? "User activated successfully." : "User de-activated successfully.";

            return (true, message);
        }

        public List<object> GetList()
        {
            //return _context.UserMasters.Include(b => b.BranchMasters).Select(b => {new BranchName = b.BranchMasters.BranchName}).ToList<object>();
            return _context.UserMasters
              .Include(p => p.BranchMasters)
              .Include(u => u.Roles).Where(u => u.Roles.RoleName != "SuperAdmin")
              .Select(p => new
              {
                  p.BranchId,
                  p.BranchMasters.BranchName,
                  p.IsActive,
                  p.UserMasterId,
                  p.Username,
                  p.FirstName,
                  p.LastName,
                  p.ContactNumber,
                  p.DateOfBirth,
                  p.Gender,
                  p.UserPassword,
                  p.UserMasterPassword,
                  p.IsFirstTimeLogin,
                  p.Ip,
                  p.UpdatedBy,
                  p.UpdatedOn,
                  CreatedOn = p.CreatedOn.ToString("MM/dd/yyyy hh:mm tt"), 
                  //p.CreatedOn = p.CreatedOn.ToString("MM/dd/yyyy hh:mm tt"), 
                  p.CreatedBy

              })
              .ToList<object>();

        }
    }
}

