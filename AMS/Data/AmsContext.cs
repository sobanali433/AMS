using Microsoft.EntityFrameworkCore;

namespace AMS.Data
{
    public class AmsContext : DbContext
    {
        public AmsContext(DbContextOptions<AmsContext> options) : base(options)
        {

        }
        public DbSet<Role> Roles { get; set; }
        public DbSet<UserMaster> UserMasters { get; set; }



    }
}
