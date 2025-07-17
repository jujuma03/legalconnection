using LC.CORE.Helpers;
using LC.CORE.Services;
using LC.ENTITIES.Models;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Areas.Admin.Models.MissionVision;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace AKDEMIC.SISCO.Areas.Admin.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.ADMIN)]
    [Route("admin/misionvision")]
    [Area("Admin")]
    public class MissionVisionController : Controller
    {
        private readonly ICloudStorageService _cloudStorageService;
        private readonly IMissionVisionService _missionVisionService;

        public MissionVisionController(ICloudStorageService cloudStorageService,
            IMissionVisionService missionVisionService)
        {
            _cloudStorageService=cloudStorageService;
            _missionVisionService = missionVisionService;
        }

        public async Task<IActionResult> Index()
        {
            var missionVision = await _missionVisionService.GetMissionVision();
            if (missionVision== null || missionVision.Count()==0)
            {
                var result = new List<MissionVisionViewModel>()
                {
                    new MissionVisionViewModel(),
                    new MissionVisionViewModel()
                };
                return View(result);
            }
            else
            {
                var result = missionVision.Select(x => new MissionVisionViewModel
                {
                    Content=x.Content,
                    Id=x.Id,
                    Title=x.Title,
                    UrlImage=x.UrlImage,
                }).ToList();
                return View(result);
            }
        }

        [HttpPost("actualizar")]
        public async Task<IActionResult> UpdateMissionVision(List<MissionVisionViewModel> model)
        {
            foreach (var item in model)
            {
                var entity = new MissionVision();
                if (item.Id != null && item.Id != Guid.Empty)
                {
                    entity = await _missionVisionService.GetMissionVisionById(item.Id);
                }

                entity.Title = item.Title;
                entity.Content = item.Content;
                if (!string.IsNullOrEmpty(item.urlCropImg))
                {
                    var imgArray1 = item.urlCropImg.Split(";");
                    var imgArray2 = imgArray1[1].Split(",");

                    var newImage = Convert.FromBase64String(imgArray2[1]);
                    using (var stream = new MemoryStream(newImage))
                    {
                        if (item.Image != null)
                        {
                            if (!item.Image.ContentType.Contains("image"))
                                return BadRequest("El archivo adjunto no es de formato ");

                            if (!string.IsNullOrEmpty(entity.UrlImage))
                                await _cloudStorageService.TryDelete(entity.UrlImage.Split('/').Last(), ConstantHelpers.CLOUD_CONTAINERS.MISSION_VISION);
                            var extension = Path.GetExtension(item.Image.FileName);
                            entity.UrlImage = await _cloudStorageService.UploadFile(stream, ConstantHelpers.CLOUD_CONTAINERS.MISSION_VISION, $"{Guid.NewGuid()}", extension);
                        }
                    }
                }
                if (item.Id != null && item.Id != Guid.Empty)
                {
                    await _missionVisionService.Update(entity);
                }
                else
                {
                    await _missionVisionService.Insert(entity);
                }
            }
            return Ok();
        }
    }
}