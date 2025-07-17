using LC.CORE.Extensions;
using LC.CORE.Helpers;
using LC.CORE.Services.Interfaces;
using LC.SERVICE.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.ADMIN + "," + ConstantHelpers.ROLES.ADVISER)]
    [Area("Admin")]
    [Route("admin/publicaciones")]
    public class PublicationController : Controller
    {
        private readonly ILawyerPublicationService _lawyerPublicationService;
        private readonly IDataTableService _dataTableService;
        private readonly ILawyerService _lawyerService;
        private readonly IUserService _userService;

        public PublicationController(
            ILawyerPublicationService lawyerPublicationService,
            IDataTableService dataTableService,
            ILawyerService lawyerService,
            IUserService userService
            )
        {
            _lawyerPublicationService = lawyerPublicationService;
            _dataTableService = dataTableService;
            _lawyerService = lawyerService;
            _userService = userService;
        }

        public IActionResult Index()
        {
            return View();
        }

        [HttpGet("get")]
        public async Task<IActionResult> GetPublicationDatatable(byte status, string search)
        {
            var sentparameters = _dataTableService.GetSentParameters();
            var result = await _lawyerPublicationService.GetPublicationsDatatable(sentparameters, status, search);
            return Ok(result);
        }

        [HttpPost("aprobar")]
        public async Task<IActionResult> ApprovePublication(Guid id)
        {
            return await ChangeStatus(id, ConstantHelpers.ENTITIES.LAWYER_PUBLICATION.STATUS.CONFIRMED);
        }

        [HttpPost("rechazar")]
        public async Task<IActionResult> RejectPublication(Guid id)
        {
            return await ChangeStatus(id, ConstantHelpers.ENTITIES.LAWYER_PUBLICATION.STATUS.DENIED);
        }

        private async Task<IActionResult> ChangeStatus(Guid id, byte status)
        {
            var publication = await _lawyerPublicationService.Get(id);
            publication.Status = status;
            publication.AnswerDate = DateTime.UtcNow;

            await _lawyerPublicationService.Update(publication);
            return Ok();
        }

        [HttpGet("get-publicacion")]
        public async Task<IActionResult> Get(Guid id)
        {
            var entity = await _lawyerPublicationService.Get(id);
            var lawyer = await _lawyerService.Get(entity.LawyerId);
            var user = await _userService.Get(lawyer.UserId);

            var data = new
            {
                entity.Id,
                entity.Description,
                entity.Title,
                entity.Topic,
                lawyer = $"{user.Name} {user.Surnames}",
                publicationDate = entity.PublicationDate.ToLocalDateTimeFormat(),
                image = entity.PhotoUrl
            };

            return Ok(data);
        }
    }
}
