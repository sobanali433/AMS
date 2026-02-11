using AMS.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

public class SeedController : Controller
{
    private readonly AmsContext _context;

    public SeedController(AmsContext context)
    {
        _context = context;
    }



    public IActionResult CreateSuperAdmin()
    {
        var forcefully = _context.Database.BeginTransaction();

        var adminRole = _context.Roles.FirstOrDefault(r => r.RoleName == "SuperAdmin");

        if (adminRole == null)
        {
            adminRole = new Role { RoleName = "SuperAdmin" };
            _context.Roles.Add(adminRole);
            _context.SaveChanges();
        }

        // 2️⃣ User exists check
        if (_context.UserMasters.Any(u => u.Username == "admin@ims.com"))
            return Content("Admin already exists");

        // 3️⃣ Password hash
        var hasher = new PasswordHasher<UserMaster>();

        var user = new UserMaster
        {
            UserMasterId = 1,
            FirstName = "Soban",
            LastName = "Ali",
            Username = "Soban0x0@gmail.com",
            ContactNumber = "0",
            IsActive = true,
            CreatedOn = DateTime.Now,
            RoleId = adminRole.RoleId,
            Gender = 1,
            BranchId = 1
        };

        user.UserPassword = hasher.HashPassword(user, "xps52DA54");
        user.UserMasterPassword = hasher.HashPassword(user, "xps52DA54");

        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT UserMasters ON");

        _context.UserMasters.Add(user);
        _context.SaveChanges();

        _context.Database.ExecuteSqlRaw("SET IDENTITY_INSERT UserMasters OFF");

        forcefully.Commit();

        return Content("SuperAdmin created");
    }

}
