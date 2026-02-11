using AMS.Models;
using AMS.Services;
using System.Runtime.Serialization.Formatters.Binary;
using Microsoft.AspNetCore.Http;
using AMS.Repository;

namespace AMS.Services
{
    public class AccountServices : IAccountServices
    {
        private readonly IAccountRepository _accountRepository;

        public AccountServices(IAccountRepository accountRepository)
        {
            _accountRepository = accountRepository;
        }

        public async Task<UserMasterModel?> LoginAsync(string username, string password)
        {
            var userEntity = await _accountRepository.GetByUsernameAndPasswordAsync(username, password);

            if (userEntity == null) return null;

            var model = new UserMasterModel
            {
                UserMasterId = userEntity.UserMasterId,
                Username = userEntity.Username,
                Userpassword = userEntity.UserPassword,
                FirstName = userEntity.FirstName,
                LastName = userEntity.LastName,
                RoleName = userEntity.Roles?.RoleName,
                IsActive = userEntity.IsActive
            };
            return model;
        }

    }
}
