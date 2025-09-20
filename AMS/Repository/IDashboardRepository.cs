using AMS.Data;

namespace AMS.Repository
{
    public interface IDashboardRepository
   {
        //Task<UserMaster?> HeaderlayoutAsync(string fullname, string rolename, string lastname);   
        Task<UserMaster?> HeaderlayoutAsync(string username);
    }
}
    