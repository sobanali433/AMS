using AMS.Data;

namespace AMS.Repository
{
    public interface IUserRepository
    {
        List<UserMaster> GetList();
        UserMaster GetById(int id);
        IEnumerable<Role> GetRoles();
        Task<bool> AddUserAsync(UserMaster user);
        Task<bool> UpdateUserAsync(UserMaster user);
        Task<UserMaster?> GetByIdAsync(int id);

    }
}
