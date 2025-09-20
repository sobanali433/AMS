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


    }
}

