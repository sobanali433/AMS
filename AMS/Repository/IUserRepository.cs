using AMS.Data;
using AMS.Models;

namespace AMS.Repository
{
    public interface IUserRepository
    {
        //List<UserMaster> GetAllUsersWithoutSuperAdmin();

        List<UserMaster> GetList();
        UserMaster GetById(int id);
        IEnumerable<Role> GetRoles();
        IEnumerable<BranchMaster> GetBranches();
        Task<bool> UpdateUserAsync(UserMaster user);
        Task<UserMaster?> GetByIdAsync(int id);
        Task<bool> AddUserAsync(UserMaster model);


    }
}
