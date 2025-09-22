using AMS.Data;

namespace AMS.Repository
{
    public interface IUserRepository
    {
        List<UserMaster> GetList();
        UserMaster GetById(int id);
        IEnumerable<Role> GetRoles();

    }
}
