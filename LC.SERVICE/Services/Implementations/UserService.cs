using LC.CORE.Structs;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Interfaces;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LC.SERVICE.Services.Implementations
{
    public class UserService : IUserService
    {
        private readonly IUserRepository _userRepository;

        public UserService(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IdentityResult> AddToRole(ApplicationUser user, string role)
            => await _userRepository.AddToRole(user, role);

        public async Task<bool> AnyByUsername(string username)
            => await _userRepository.AnyByUsername(username);

        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string currentPassword, string newPssword)
        {
            return await _userRepository.CheckPasswordAsync(user, currentPassword, newPssword);
        }

        public async Task<IdentityResult> ConfirmEmail(ApplicationUser user, string code)
            => await _userRepository.ConfirmEmail(user, code);

        public async Task<ApplicationUser> FindByEmailAsync(string value)
        {
            return await _userRepository.FindByEmailAsync(value);
        }

        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return await _userRepository.FindByNameAsync(userName);
        }

        public async Task<string> GenerateEmailConfimationToken(ApplicationUser user)
            => await _userRepository.GenerateEmailConfimationToken(user);

        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            return await _userRepository.GeneratePasswordResetTokenAsync(user);
        }

        public async Task<ApplicationUser> Get(string userId)
            => await _userRepository.Get(userId);

        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            return await _userRepository.GetRolesAsync(user);
        }

        public async Task<ApplicationUser> GetUserByClaim(ClaimsPrincipal user)
            => await _userRepository.GetUserByClaim(user);

        public async Task<IdentityResult> Insert(ApplicationUser user, string password)
            => await _userRepository.Insert(user, password);

        public async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string code, string password)
        {
            return await _userRepository.ResetPasswordAsync(user, code, password);
        }

        public async Task Update(ApplicationUser user)
            => await _userRepository.Update(user);

        public async Task UpdatePassword(ApplicationUser user, string newPassword)
        {
            await _userRepository.UpdatePassword(user, newPassword);
        }

        public async Task<DataTablesStructs.ReturnedData<object>> GetUserNewDatatable(DataTablesStructs.SentParameters parameters, string searchValue)
            => await _userRepository.GetUserNewDatatable(parameters, searchValue);
    }
}
