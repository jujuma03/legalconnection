using LC.CORE.Structs;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Interfaces
{
    public interface IUserRepository : IRepository<ApplicationUser>
    {
        Task<IdentityResult> ConfirmEmail(ApplicationUser user, string code);
        Task<IdentityResult> Insert(ApplicationUser user, string password);
        Task<string> GenerateEmailConfimationToken(ApplicationUser user);
        Task<ApplicationUser> FindByEmailAsync(string value);
        Task<IdentityResult> AddToRole(ApplicationUser user, string role);
        Task<ApplicationUser> GetUserByClaim(ClaimsPrincipal user);
        Task<bool> CheckPasswordAsync(ApplicationUser user, string currentPassword, string newPssword);
        Task UpdatePassword(ApplicationUser user, string newPassword);
        Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user);
        Task<ApplicationUser> FindByNameAsync(string userName);
        Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string code, string password);
        Task<IList<string>> GetRolesAsync(ApplicationUser user);
        Task<bool> AnyByUsername(string username);
        Task<DataTablesStructs.ReturnedData<object>> GetUserNewDatatable(DataTablesStructs.SentParameters parameters, string searchValue);
    }
}
