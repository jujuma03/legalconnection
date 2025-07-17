using LC.ENTITIES.Models;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Models.Qualification;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Controllers
{
    [Route("calificaciones")]
    public class QualificationController : Controller
    {
        private readonly ILawyerQualificationService _lawyerQualificationService;
        private readonly ILawyerService _lawyerService;
        private readonly IClientService _clientService;
        private readonly IUserService _userService;

        public QualificationController(
            ILawyerQualificationService lawyerQualificationService,
            ILawyerService lawyerService,
            IClientService clientService,
            IUserService userService
            )
        {
            _lawyerQualificationService = lawyerQualificationService;
            _lawyerService = lawyerService;
            _clientService = clientService;
            _userService = userService;
        }

        [HttpGet("{legalCaseId}/{clientId}/{lawyerId}")]
        public async Task<IActionResult> Index(Guid legalCaseId, Guid clientId, Guid lawyerId)
        {
            var client = await _clientService.Get(clientId);
            var clientUser = await _userService.Get(client.UserId);
            var lawyer = await _lawyerService.Get(lawyerId);
            var lawyerUser = await _userService.Get(lawyer.UserId);

            if (await _lawyerQualificationService.AnyQualificationByFilter(legalCaseId, clientId, lawyerId))
                return RedirectToAction("Index", "Error", new { statusCode = 423 });

            var model = new QualificationViewModel
            {
                Client = new ClientViewModel
                {
                    Id = client.Id,
                    FullName = $"{clientUser.Name} {clientUser.Surnames}"
                },
                Lawyer = new LawyerViewModel
                {
                    Id = lawyer.Id,
                    FullName = $"{lawyerUser.Name} {lawyerUser.Surnames}",
                    PhotoUrl = lawyerUser.Picture
                },
                LegalCaseId = legalCaseId
            };

            return View(model);
        }

        [HttpPost("enviar")]
        public async Task<IActionResult> Send(QualificationViewModel model)
        {
            var entity = new LawyerQualification
            {
                ClientId = model.Client.Id,
                LawyerId = model.Lawyer.Id,
                LegalCaseId = model.LegalCaseId,
                Commentary = model.Commentary,
                Qualification = model.Qualification
            };

            var result = await _lawyerQualificationService.SendQualification(entity);
            if (!result.Success)
                return BadRequest(result.Message);

            return Ok();
        }
    }
}
