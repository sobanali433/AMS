using AMS.Data;
using AMS.Models;
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
            return _context.UserMasters.FirstOrDefault(u => u.UserMasterId == id);
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

        public List<UserMaster> GetList()
        {
            return _context.UserMasters.AsNoTracking().Where(u => u.Roles.RoleName != "SuperAdmin").ToList();
        }

        //public List<UserMaster> GetAllUsersWithoutSuperAdmin()
        //{
        //    return _context.UserMasters.Include(u => u.Roles.RoleId ==1).Where(u => u.Roles.RoleName != "SuperAdmin" ).ToList();
        //}


    }
}

