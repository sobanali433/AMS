using AMS.Data;
using AMS.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace AMS.Repository
{
    public class AccountRepository : IAccountRepository
    {
        private readonly AmsContext _context;
        public AccountRepository(AmsContext context)
        {
            _context = context;
        }
        
        public async Task<UserMaster?> GetByUsernameAsync(string username, string rolename)
        {
            return await _context.UserMasters
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u => u.Username == username || u.Role.RoleName == rolename);
        }
       

       
        public async Task<UserMaster?> GetByUsernameAndPasswordAsync(string username, string password)
        {
            var user = await _context.UserMasters
                .Include(u => u.Role)
                .FirstOrDefaultAsync(u =>
                    u.Username == username &&
                    u.UserPassword == password);

            if (user != null)
            {
                Console.WriteLine($"✅ User Found: {user.Username}, Role: {user.Role.RoleName}");
            }
            else
            {
                Console.WriteLine("❌ No user found with given credentials!");
            }

            return user;
        }

        public async Task<UserMaster?> GetByUsernameAsync(string username)
        {
            var user = await _context.UserMasters
          .Include(u => u.Role)
          .FirstOrDefaultAsync(u => u.Username == username);

            if (user == null || user.Role == null)
                return null;

            return user;

        }
        }
}
