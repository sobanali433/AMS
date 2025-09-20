using AMS.Models;

namespace AMS.Services
{
    public interface IAccountServices
    {
        Task<UserMasterModel?> LoginAsync(string username, string password);

    }
}
