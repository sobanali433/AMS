using AMS.Data;
using AMS.Migrations;
using Microsoft.EntityFrameworkCore;

namespace AMS.Repository
{
    public class DashboardRepository : IDashboardRepository
    {
        private readonly AmsContext _context;
        public DashboardRepository(AmsContext context)
        {
            _context = context;
        }
        public  Task<UserMaster?> HeaderlayoutAsync(string username)
        {
            return  _context.UserMasters
                .Include(u => u.Role) 
                .FirstOrDefaultAsync(u => u.Username == username);
        }
    }
}
