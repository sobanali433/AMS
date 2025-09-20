using AMS.Data;
using AMS.Models;

namespace AMS.Repository
{
    public interface IAccountRepository
    {
        //UserMasterModel GetUserById(int id    
        Task<UserMaster?> GetByUsernameAsync(string username, string rolename);
        Task<UserMaster?> GetByUsernameAndPasswordAsync(string username, string password);

    }
}
