using LC.CORE.Extensions;
using LC.CORE.Helpers;
using LC.ENTITIES.Models;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Areas.Client.Models.Profile;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Client.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.CLIENT)]
    [Area("Client")]
    [Route("perfil")]
    public class ProfileController : Controller
    {
        private readonly IUserService _userService;
        private readonly IDistrictService _districtService;
        private readonly IClientService _clientService;

        public ProfileController(
            IClientService clientService,
            IUserService userService,
            IDistrictService districtService
            )
        {
            _userService = userService;
            _clientService = clientService;
            _districtService = districtService;
        }

        public async Task<IActionResult> Index()
        {
            var user = await _userService.GetUserByClaim(User);
            var client = await _clientService.GetByUserId(user.Id);

            var ubigeoQuery = _districtService.GetAsQueryable();
            var ubigeoData = await ubigeoQuery.Where(x => x.Id == user.DistrictId)
                .Select(x => new
                {
                    district = x.Name,
                    districtId = x.Id,
                    province = x.Province.Name,
                    provinceId = x.ProvinceId,
                    department = x.Province.Department.Name,
                    departmentId = x.Province.DepartmentId
                })
                .FirstOrDefaultAsync();

            var model = new ProfileViewModel
            {
                ClientId = client.Id,
                PersonalInformation = new PersonalInformationViewModel
                {
                    ClientId = client.Id,
                    BirthDate =user.BirthDate!=null?user.BirthDate==DateTime.MinValue ? DateTime.UtcNow.AddYears(20).ToLocalDateFormat() : user.BirthDate.ToLocalDateFormat():"",
                    DNI = user.Document,
                    DocumentType =ConstantHelpers.DOCUMENT_TYPE.VALUES.ContainsKey(user.DocumentType)? ConstantHelpers.DOCUMENT_TYPE.VALUES[user.DocumentType] : ConstantHelpers.DOCUMENT_TYPE.VALUES[ConstantHelpers.DOCUMENT_TYPE.DNI],
                    Department = ubigeoData?.department??"",
                    DepartmentId = ubigeoData?.departmentId,
                    District = ubigeoData?.district??"",
                    DistrictId = ubigeoData?.districtId,
                    Email = user.Email,
                    HouseNumber = user.HouseNumber??"",
                    Name = user.Name,
                    PhoneNumber = user.PhoneNumber??"",
                    Province = ubigeoData?.province ??"",
                    ProvinceId = ubigeoData?.provinceId,
                    Sex = user.Sex,
                    Surnames = user.Surnames
                },
            };

            return View(model);
        }

        #region -- Información Personal --

        [HttpGet("get-informacion-personal")]
        public async Task<IActionResult> GetPersonalInformation()
        {
            var user = await _userService.GetUserByClaim(User);
            var lawyer = await _clientService.GetByUserId(user.Id);
            var ubigeoQuery = _districtService.GetAsQueryable();
            var ubigeoData = await ubigeoQuery.Where(x => x.Id == user.DistrictId)
                .Select(x => new
                {
                    district = x.Name,
                    districtId = x.Id,
                    province = x.Province.Name,
                    provinceId = x.ProvinceId,
                    department = x.Province.Department.Name,
                    departmentId = x.Province.DepartmentId
                })
                .FirstOrDefaultAsync();

            var model = new PersonalInformationViewModel
            {
                BirthDate = user.BirthDate.ToLocalDateFormat(),
                DNI = user.Document,
                Department = ubigeoData.department,
                DepartmentId = ubigeoData.departmentId,
                District = ubigeoData.district,
                DistrictId = ubigeoData.districtId,
                Email = user.Email,
                HouseNumber = user.HouseNumber,
                Name = user.Name,
                PhoneNumber = user.PhoneNumber,
                Province = ubigeoData.province,
                ProvinceId = ubigeoData.provinceId,
                Sex = user.Sex,
                Surnames = user.Surnames
            };

            return Ok(model);
        }

        [HttpPost("actualizar-informacion-personal")]
        public async Task<IActionResult> UpdatePersonalInformation(PersonalInformationViewModel model)
        {
            var client = await _clientService.Get(model.ClientId);
            var user = await _userService.Get(client.UserId);

            user.BirthDate = ConvertHelpers.DatepickerToUtcDateTime(model.BirthDate);
            user.Document = model.DNI;
            user.DistrictId = model.DistrictId;
            user.HouseNumber = model.HouseNumber;
            user.Name = model.Name;
            user.PhoneNumber = model.PhoneNumber;
            user.Sex = model.Sex;
            user.Surnames = model.Surnames;

            await _userService.Update(user);
            return Ok();
        }

        [HttpPost("cambiar-password")]
        public async Task<IActionResult> ChangePassword(ChangePasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                var user = await _userService.GetUserByClaim(User);

                var currentPasswordIsCorrect = await _userService.CheckPasswordAsync(user, model.CurrentPassword, model.NewPassword);

                if (!currentPasswordIsCorrect)
                    return BadRequest("Revise la información ingresada");
                await _userService.UpdatePassword(user, model.NewPassword);

                return Ok();
            }
            return BadRequest("Por favor verifique los datos ingresados");
        }
        #endregion
    }
}
