using LC.CORE.Extensions;
using LC.CORE.Helpers;
using LC.CORE.Structs;
using LC.DATABASE.Data;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using LC.REPOSITORY.Repositories.Interfaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Implementations
{
    public class UserRepository : Repository<ApplicationUser>, IUserRepository
    {
        private readonly UserManager<ApplicationUser> _userManager;

        public UserRepository(
            LegalConnectionContext context,
            UserManager<ApplicationUser> userManager
            ) : base(context)
        {
            _userManager = userManager;
        }

        public override async Task<ApplicationUser> Get(string userId)
            => await _userManager.FindByIdAsync(userId);
        public async Task<IdentityResult> ConfirmEmail(ApplicationUser user, string code)
            => await _userManager.ConfirmEmailAsync(user, code);
        public async Task<IdentityResult> Insert(ApplicationUser user, string password)
            => await _userManager.CreateAsync(user, password);
        public async Task<string> GenerateEmailConfimationToken(ApplicationUser user)
            => await _userManager.GenerateEmailConfirmationTokenAsync(user);
        public async Task<ApplicationUser> FindByEmailAsync(string value)
        {
            return await _userManager.FindByEmailAsync(value);
        }
        public async Task<IdentityResult> AddToRole(ApplicationUser user, string role)
            => await _userManager.AddToRoleAsync(user, role);
        public async Task<ApplicationUser> GetUserByClaim(ClaimsPrincipal user)
            => await _userManager.GetUserAsync(user);
        public async Task<bool> CheckPasswordAsync(ApplicationUser user, string currentPassword, string newPassword)
        {
            var passwordValidator = new PasswordValidator<ApplicationUser>();
            var currentPasswordIsCorrect = await _userManager.CheckPasswordAsync(user, currentPassword);

            var newPasswordIsValid = passwordValidator.ValidateAsync(_userManager, user, newPassword).Result.Succeeded;

            if (!currentPasswordIsCorrect || !newPasswordIsValid)
                return false;
            return true;
        }
        public async Task UpdatePassword(ApplicationUser user, string newPassword)
        {
            user.PasswordHash = _userManager.PasswordHasher.HashPassword(user, newPassword);

            await this.Update(user);
        }
        public async Task<string> GeneratePasswordResetTokenAsync(ApplicationUser user)
        {
            return await _userManager.GeneratePasswordResetTokenAsync(user);
        }
        public async Task<ApplicationUser> FindByNameAsync(string userName)
        {
            return await _userManager.FindByNameAsync(userName);
        }
        public async Task<IdentityResult> ResetPasswordAsync(ApplicationUser user, string code, string password)
        {
            return await _userManager.ResetPasswordAsync(user, code, password);
        }
        public async Task<IList<string>> GetRolesAsync(ApplicationUser user)
        {
            return await _userManager.GetRolesAsync(user);
        }
        public async Task<bool> AnyByUsername(string username)
            => await _context.Users.AnyAsync(x => x.UserName.ToLower() == username.ToLower());

        public async Task<DataTablesStructs.ReturnedData<object>> GetUserNewDatatable(DataTablesStructs.SentParameters parameters, string searchValue)
        {
            Expression<Func<ApplicationUser, dynamic>> orderByPredicate = null;

            switch (parameters.OrderColumn)
            {
                case "0":
                    orderByPredicate = (x) => x.FullName;
                    break;
                case "1":
                    orderByPredicate = (x) => x.Email;
                    break;
                case "2":
                    orderByPredicate = (x) => x.CreatedAt;
                    break;
                case "3":
                    orderByPredicate = (x) => x.PhoneNumber;
                    break;
                case "4":
                    orderByPredicate = (x) => x.Document;
                    break;
                case "5":
                    orderByPredicate = (x) => x.UserRoles.Any(y => y.Role.Name == ConstantHelpers.ROLES.LAWYER) ? "Abogado" : x.UserRoles.Any(y => y.Role.Name == ConstantHelpers.ROLES.ADMIN) ? "Administrador" : "Cliente";
                    break;
                default:
                    orderByPredicate = (x) => x.CreatedAt;
                    break;
            }

            var query = _context.Users
                 .AsNoTracking();


            if (!string.IsNullOrEmpty(searchValue))
                query = query.Where(x => x.FullName.ToUpper().Contains(searchValue.ToUpper()));

            var recordsTotal = await query.CountAsync(); 

            query = query.AsQueryable();

            var data = await query
                .OrderByCondition(parameters.OrderDirection, orderByPredicate)
                .Skip(parameters.PagingFirstRecord)
                .Take(parameters.RecordsPerDraw)
                    .Select(x => new
                    {
                        id = x.Id,
                        name = x.Name,
                        surname = x.Surnames,
                        fullName = x.FullName,
                        email = x.Email,
                        phone = x.PhoneNumber,
                        document = x.Document,
                        date = x.CreatedAt.ToLocalDateTimeFormat(),
                        createdAt = x.CreatedAt,
                        type = x.UserRoles.Any(y => y.Role.Name == ConstantHelpers.ROLES.LAWYER) ? "Abogado" : x.UserRoles.Any(y => y.Role.Name == ConstantHelpers.ROLES.ADMIN) ? "Administrador" : "Cliente"
                    })
                .ToListAsync();

            var recordsFiltered = data.Count;

            var result = new DataTablesStructs.ReturnedData<object>
            {
                Data = data,
                DrawCounter = parameters.DrawCounter,
                RecordsFiltered = recordsTotal,
                RecordsTotal = recordsTotal,
            };
            return result;
        }
    }
}
