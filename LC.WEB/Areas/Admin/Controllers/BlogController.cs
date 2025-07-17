using LC.CORE.Extensions;
using LC.CORE.Helpers;
using LC.CORE.Services;
using LC.CORE.Services.Interfaces;
using LC.ENTITIES.Models;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Areas.Admin.Models.Blog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.ADMIN)]
    [Route("admin/blog")]
    [Area("Admin")]
    public class BlogController : Controller
    {
        private readonly IBlogService _blogService;
        private readonly ICloudStorageService _cloudStorageService;
        private readonly ILawyerPublicationService _lawyerPublicationService;
        private readonly IDataTableService _dataTableService;
        private readonly ILawyerService _lawyerService;
        private readonly IUserService _userService;
        private readonly IExternalPublicationService _externalPublicationService;

        public BlogController(
            IBlogService blogService,
            ICloudStorageService cloudStorageService,
            ILawyerPublicationService lawyerPublicationService,
            IDataTableService dataTableService,
            ILawyerService lawyerService,
            IUserService userService,
            IExternalPublicationService externalPublicationService
            )
        {
            _blogService = blogService;
            _cloudStorageService = cloudStorageService;
            _lawyerPublicationService = lawyerPublicationService;
            _dataTableService = dataTableService;
            _lawyerService = lawyerService;
            _userService = userService;
            _externalPublicationService = externalPublicationService;
        }

        public IActionResult Index()
            => View();

        [HttpGet("blog-get")]
        public async Task<IActionResult> GetBlogs(string searchValue)
        {
            var parameters = _dataTableService.GetSentParameters();
            var result = await _blogService.GetBlogDatatable(parameters, searchValue);
            return Ok(result);
        }

        [HttpGet("blog-detalle")]
        public async Task<IActionResult> BlogDetail(Guid id)
        {
            var blog = await _blogService.Get(id);
            var model = new BlogViewModel();
            model.Id = blog.Id;

            if(blog.Type == ConstantHelpers.ENTITIES.BLOG.TYPES.EXTERNALPUBLICATION)
            {
                var externalPublication = await _externalPublicationService.Get(blog.ExternalPublicationId.Value);
                model.Description = externalPublication.Description;
                model.LawyerFullName = externalPublication.LawyerFullName;
                model.LawyerPhotoUrl = externalPublication.LawyerPhotoUrl;
                model.PhotoUrl = externalPublication.PhotoUrl;
                model.PublicationDate = externalPublication.PublicationDate.ToLocalDateFormat();
                model.Title = externalPublication.Title;
                model.Topic = externalPublication.Topic;
            }
            else
            {
                var lawyerPublication = await _lawyerPublicationService.Get(blog.LawyerPublicationId.Value);
                var lawyer = await _lawyerService.Get(lawyerPublication.LawyerId);
                var user = await _userService.Get(lawyer.UserId);
                model.Description = lawyerPublication.Description;
                model.Title = lawyerPublication.Title;
                model.Topic = lawyerPublication.Topic;
                model.LawyerFullName = $"{user.Name} {user.Surnames}";
                model.LawyerPhotoUrl = user.Picture;
                model.PublicationDate = lawyerPublication.PublicationDate.ToLocalDateFormat();
                model.PhotoUrl = lawyerPublication.PhotoUrl;
            }

            return View(model);
        }

        [HttpPost("blog-eliminar")]
        public async Task<IActionResult> Delete(Guid id)
        {
            var blog = await _blogService.Get(id);
            await _blogService.Delete(blog);
            return Ok();
        }

        [HttpGet("publicaciones-abogado")]
        public IActionResult LawyerPublication()
            => View();

        [HttpGet("publicaciones-abogado-get")]
        public async Task<IActionResult> GetLawyerPublications(string searchValue)
        {
            var parameters = _dataTableService.GetSentParameters();
            var result = await _lawyerPublicationService.GetPublicationsDatatable(parameters, ConstantHelpers.ENTITIES.LAWYER_PUBLICATION.STATUS.CONFIRMED, searchValue);
            return Ok(result);
        }

        [HttpGet("publicaciones-abogado-detalle")]
        public async Task<IActionResult> LawyerPublicationDetail(Guid id)
        {
            var publication = await _lawyerPublicationService.Get(id);
            var lawyer = await _lawyerService.Get(publication.LawyerId);
            var user = await _userService.Get(lawyer.UserId);
            var isInBlog = await _blogService.AnyByTypeAndEntity(ConstantHelpers.ENTITIES.BLOG.TYPES.LAWYERPUBLICATION, publication.Id);
            var model = new LawyerPublicationViewModel
            {
                Description = publication.Description,
                Id = publication.Id,
                Lawyer = $"{user.Name} {user.Surnames}",
                LawyerPhotoUrl = user.Picture,
                PublicationDate = publication.PublicationDate.ToLocalDateFormat(),
                PhotoUrl = publication.PhotoUrl,
                Title = publication.Title,
                Topic = publication.Topic,
                IsInBlog = isInBlog
            };

            return View(model);
        }

        [HttpPost("agregar-blog-publicacion-abogado")]
        public async Task<IActionResult> AddBlogLawyerPublication(Guid id)
        {
            if (await _blogService.AnyByTypeAndEntity(ConstantHelpers.ENTITIES.BLOG.TYPES.LAWYERPUBLICATION, id))
                return BadRequest("La publicación ya se encuentra registrada en el blog.");

            var blog = new Blog
            {
                LawyerPublicationId = id,
                Type = ConstantHelpers.ENTITIES.BLOG.TYPES.LAWYERPUBLICATION
            };

            await _blogService.Insert(blog);
            return Ok();
        }

        [HttpGet("publicaciones-externas")]
        public IActionResult ExternalPublication()
            => View();

        [HttpGet("publicaciones-externas-get")]
        public async Task<IActionResult> GetExternalPublications(string searchValue)
        {
            var parameters = _dataTableService.GetSentParameters();
            var result = await _externalPublicationService.GetExternalPublicationDatatable(parameters, searchValue);
            return Ok(result);
        }

        [HttpGet("publicacines-externas-agregar")]
        public IActionResult AddExternalPublication()
            => View();

        [HttpPost("publicacion-external-add")]
        public async Task<IActionResult> AddExternalPublication(ExternalPublicationViewModel model)
        {
            var entity = new ExternalPublication
            {
                Description = model.Description,
                CreatedAt= DateTime.UtcNow,
                LawyerFullName = model.LawyerFullName,
                PublicationDate = ConvertHelpers.DatepickerToUtcDateTime(model.PublicationDate),
                Title = model.Title,
                Topic = model.Topic
            };
            if (!string.IsNullOrEmpty(model.urlPhotoCropImg))
            {
                var imgArray1 = model.urlPhotoCropImg.Split(";");
                var imgArray2 = imgArray1[1].Split(",");

                var newImage = Convert.FromBase64String(imgArray2[1]);
                using (var stream = new MemoryStream(newImage))
                {
                    var extension = Path.GetExtension(model.Photo.FileName);
                    entity.PhotoUrl = await _cloudStorageService.UploadFile(
                        stream,
                        ConstantHelpers.CLOUD_CONTAINERS.LAWYER_PUBLICATIONS,
                         $"externo-{Guid.NewGuid()}",
                        extension
                        );
                }
            }
            if (!string.IsNullOrEmpty(model.urlLawyerPhotoCropImg))
            {
                var imgArray1 = model.urlLawyerPhotoCropImg.Split(";");
                var imgArray2 = imgArray1[1].Split(",");

                var newImage = Convert.FromBase64String(imgArray2[1]);
                using (var stream = new MemoryStream(newImage))
                {
                    var extension = Path.GetExtension(model.LawyerPhoto.FileName);
                    entity.LawyerPhotoUrl = await _cloudStorageService.UploadFile(
                        stream,
                        ConstantHelpers.CLOUD_CONTAINERS.LAWYER_PUBLICATIONS,
                         $"externo-{Guid.NewGuid()}",
                        extension
                        );
                }
            }

            await _externalPublicationService.Insert(entity);
            return Ok();
        }
        [HttpPost("publicacion-externa-eliminar")]
        public async Task<IActionResult> DeleteExternalPublication(Guid id)
        {
            if (await _blogService.AnyByTypeAndEntity(ConstantHelpers.ENTITIES.BLOG.TYPES.EXTERNALPUBLICATION, id))
                return BadRequest("La publicación se encuentra en el blog.");

            var entity = await _externalPublicationService.Get(id);

            if (!string.IsNullOrEmpty(entity.PhotoUrl))
                await _cloudStorageService.TryDelete(entity.PhotoUrl, ConstantHelpers.CLOUD_CONTAINERS.LAWYER_PUBLICATIONS);

            if(!string.IsNullOrEmpty(entity.LawyerPhotoUrl))
                await _cloudStorageService.TryDelete(entity.LawyerPhotoUrl, ConstantHelpers.CLOUD_CONTAINERS.PROFILE);

            await _externalPublicationService.Delete(entity);
            return Ok();
        }

        [HttpGet("publicaciones-externas-detalle")]
        public async Task<IActionResult> ExternalPublicationDetail(Guid id)
        {
            var entity = await _externalPublicationService.Get(id);
            var isInBlog = await _blogService.AnyByTypeAndEntity(ConstantHelpers.ENTITIES.BLOG.TYPES.EXTERNALPUBLICATION, entity.Id);

            var model = new ExternalPublicationViewModel
            {
                Description = entity.Description,
                Id = entity.Id,
                LawyerFullName = entity.LawyerFullName,
                LawyerPhotoUrl = entity.LawyerPhotoUrl,
                PhotoUrl = entity.PhotoUrl,
                PublicationDate = entity.PublicationDate.ToLocalDateFormat(),
                Title = entity.Title,
                Topic = entity.Topic,
                IsInBlog = isInBlog
            };

            return View(model);
        }

        [HttpPost("agregar-blog-publicacion-externa")]
        public async Task<IActionResult> AddBlogExternalPublication(Guid id)
        {
            if (await _blogService.AnyByTypeAndEntity(ConstantHelpers.ENTITIES.BLOG.TYPES.EXTERNALPUBLICATION, id))
                return BadRequest("La publicación ya se encuentra registrada en el blog.");

            var blog = new Blog
            {
                ExternalPublicationId = id,
                Type = ConstantHelpers.ENTITIES.BLOG.TYPES.EXTERNALPUBLICATION
            };

            await _blogService.Insert(blog);
            return Ok();
        }
    }
}
