using AMS.Data;

namespace AMS.Repository
{
    public interface IAccountRepository
    {
        //UserMasterModel GetUserById(int id    
        Task<UserMaster?> GetByUsernameAsync(string username);
        Task<UserMaster?> GetByUsernameAndPasswordAsync(string username, string password);

    }
}
