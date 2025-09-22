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

        public List<UserMaster> GetList()
        {
            return _context.UserMasters.ToList();
        }
        public UserMaster GetById(int id)
        {
            return _context.UserMasters.FirstOrDefault(u => u.UserMasterId == id);
        }
        public IEnumerable<Role> GetRoles()
        {
            return _context.Roles.ToList();
        }


    }
}

