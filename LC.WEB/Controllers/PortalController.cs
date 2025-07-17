using LC.CORE.Extensions;
using LC.CORE.Helpers;
using LC.CORE.Services.Interfaces;
using LC.ENTITIES.Custom;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Extensions;
using LC.WEB.Models.Home;
using LC.WEB.Models.Portal;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Controllers
{
    public class PortalController : Controller
    {
        private readonly IBannerService _bannerService;
        private readonly ISectionItemService _sectionItemService;
        private readonly ILawyerService _lawyerService;
        private readonly ILawyerQualificationService _lawyerQualificationService;
        private readonly ILawyerSpecialityThemeService _lawyerSpecialtyThemeService;
        private readonly ILawyerPublicationService _lawyerPublicationService;
        private readonly IMissionVisionService _missionVisionService;
        private readonly IHowItWorksStepService _howItWorksStepService;
        private readonly IBlogService _blogService;
        private readonly IUserService _userService;
        private readonly IDistrictService _districtService;
        private readonly IExternalPublicationService _externalPublicationService;
        private readonly IFrequentQuestionService _frequentQuestionService;
        private readonly IPaginationService _paginationService;
        private readonly ISpecialityService _specialityService;
        public PortalController(IBannerService bannerService,
            ILawyerPublicationService lawyerPublicationService,
            ILawyerService lawyerService,
            ISectionItemService sectionItemService,
            IMissionVisionService missionVisionService,
            IHowItWorksStepService howItWorksStepService,
            IBlogService blogService,
            IUserService userService,
            IDistrictService districtService,
            IExternalPublicationService externalPublicationService,
            IFrequentQuestionService frequentQuestionService,
            ILawyerSpecialityThemeService lawyerSpecialtyThemeService,
            ILawyerQualificationService lawyerQualificationService,
            ISpecialityService specialityService,
             IPaginationService paginationService)
        {
            _bannerService = bannerService;
            _lawyerService = lawyerService;
            _lawyerPublicationService = lawyerPublicationService;
            _sectionItemService = sectionItemService;
            _missionVisionService = missionVisionService;
            _howItWorksStepService = howItWorksStepService;
            _blogService = blogService;
            _userService = userService;
            _districtService = districtService;
            _externalPublicationService = externalPublicationService;
            _frequentQuestionService = frequentQuestionService;
            _paginationService = paginationService;
            _lawyerSpecialtyThemeService = lawyerSpecialtyThemeService;
            _lawyerQualificationService = lawyerQualificationService;
            _specialityService = specialityService;
        }
        public async Task<IActionResult> Index()
        {
            if (!ConstantHelpers.ShowAllOptions)
            {
                return RedirectToAction(nameof(this.Blog));
            }

            var banners = await _bannerService.GetAllBannersActive();
            var lawyers = await _lawyerService.GetAllToDirectory();
            var specialties = await _specialityService.GetAll();

            var model = new HomeViewModel
            {
                Banner = banners.Select(x => new BannerViewModel
                {
                    Id = x.Id,
                    Headline = x.Headline,
                    Description = x.Description,
                    UrlImage = x.UrlImage,
                    UrlDirection = x.UrlDirection,
                    NameDirection = x.NameDirection,
                    SequenceOrderId = x.SequenceOrder.HasValue ? x.SequenceOrder.Value : 0,
                    StatusDirectionId = x.StatusDirection == 1 ? true : false,
                    StatusId = x.Status == 1 ? true : false,
                    RouteTypeId = x.RouteType == 1 ? true : false,
                }).ToList(),
                TopLawyers = lawyers
                .Take(5)
                .Select(x => new LawyerTemp
                {
                    Id = x.Id,
                    Name = x.Name,
                    Surnames = x.Surnames,
                    Department = x.Department,
                    District = x.District,
                    Price = x.Price,
                    LastConnection = x.LastConnection,
                    Cases = x.Cases,
                    AboutLawyer = x.AboutLawyer,
                    RegisteredAt = x.RegisteredAt,
                    UrlImage = x.UrlImage,
                    Qualification = _lawyerQualificationService.GetTotalQualification(x.Id).Result,
                    Specialties = x.Specialties.Select(y => new LawyerSpecialtiesThemesTemp
                    {
                        Name = y.Name
                    }).ToList(),
                    Themes = x.Themes.Select(y => new LawyerSpecialtiesThemesTemp
                    {
                        Name = y.Name
                    }).ToList()
                })
                .ToList(),
                Specialties = specialties.Select(x => x.ColloquialName).ToList()
            };

            return View(model);
        }
        [HttpGet("como-funciona")]
        public IActionResult HowItWorks()
        {
            return View();
        }
        [HttpGet("como-funciona-abogado")]
        public IActionResult HowItWorksAsLawyer()
        {
            return View();
        }
        [HttpGet("quienes-somos")]
        public IActionResult AboutUs()
        {
            return View();
        }
        [HttpGet("blog")]
        public async Task<IActionResult> Blog()
        {
            var publs = await _blogService.GetAll();
            return View(publs);
        }
        [HttpGet("get-blogs")]
        public async Task<IActionResult> GetBlog()
        {
            var parameters = _paginationService.GetSentParameters();
            var publs = await _blogService.GetLawyerPublications(parameters);
            var view = await this.RenderViewToStringAsync("/Views/Portal/Partials/_Blogs.cshtml", publs);
            return Ok(view);
        }
        [HttpGet("blog-detalle")]
        public async Task<IActionResult> BlogDetail(Guid id)
        {
            var blog = await _blogService.Get(id);
            var model = new BlogDetailViewModel();
            if (blog.Type == ConstantHelpers.ENTITIES.BLOG.TYPES.EXTERNALPUBLICATION)
            {
                var entity = await _externalPublicationService.Get(blog.ExternalPublicationId.Value);
                model.Description = entity.Description;
                model.LawyerFullName = entity.LawyerFullName;
                model.LawyerPictureUrl = entity.LawyerPhotoUrl;
                model.PhotoUrl = entity.PhotoUrl;
                model.PublicationDate = entity.PublicationDate;
                model.Title = entity.Title;
                model.Topic = entity.Topic;
                model.Disctrict = "-";
                model.Department = "-";
                model.Type = ConstantHelpers.ENTITIES.BLOG.TYPES.EXTERNALPUBLICATION;
            }
            else
            {
                var entity = await _lawyerPublicationService.Get(blog.LawyerPublicationId.Value);
                var specialties = await _lawyerSpecialtyThemeService.GetSpecialitiesByLawyer(entity.LawyerId);
                var lawyer = await _lawyerService.Get(entity.LawyerId);
                var user = await _userService.Get(lawyer.UserId);
                var ubigeoQuery = _districtService.GetAsQueryable();
                var ubigeoData = ubigeoQuery.Where(x => x.Id == user.DistrictId)
                    .Select(x => new
                    {
                        disctrict = x.Name,
                        department = x.Province.Department.Name
                    }).FirstOrDefault();
                model.LawyerId = entity.LawyerId;
                model.AboutLawyer = lawyer.AboutMe;
                model.Description = entity.Description;
                model.LawyerFullName = $"{user.Name} {user.Surnames}";
                model.LawyerPictureUrl = user.Picture;
                model.PhotoUrl = entity.PhotoUrl;
                model.PublicationDate = entity.PublicationDate;
                model.Title = entity.Title;
                model.Topic = entity.Topic;
                model.Disctrict = ubigeoData.disctrict;
                model.Department = ubigeoData.department;
                model.SpecialtyName = string.Join(",", specialties.Select(x => x.SpecialityTheme.ColloquialName));
                model.Themes = specialties.Select(x => x.SpecialityTheme.ColloquialName).ToList();

                var qualif = await _lawyerQualificationService.GetTotalQualification(entity.LawyerId);
                model.Qualification = qualif;
                model.Type = ConstantHelpers.ENTITIES.BLOG.TYPES.LAWYERPUBLICATION;
            }

            return View(model);
        }
        [HttpGet("get-como-funciona")]
        public async Task<IActionResult> GetHowItWorks()
        {
            var services = await _sectionItemService.GetActiveBySection(ConstantHelpers.ENTITIES.SECTION_ITEMS.TYPES.HOW_IT_WORKS);
            var result = services.Select(x => new SectionItemViewModel
            {
                Description = x.Description,
                Headline = x.HeadLine,
                UrlImage = x.UrlImage
            }).ToList();
            var view = await this.RenderViewToStringAsync(@"\Views\Portal\Partials\_HowItWorks.cshtml", result);
            return Ok(view);
            //var howItWorks = await _howItWorksStepService.GetAllActive(ConstantHelpers.ENTITIES.HOW_IT_WORKS.TYPE.CLIENT);
            //var view = await this.RenderViewToStringAsync(@"\Views\Portal\Partials\_HowItWorks.cshtml", howItWorks);
            //return Ok(view);
        }
        [HttpGet("get-servicios")]
        public async Task<IActionResult> GetServices()
        {
            var services = await _sectionItemService.GetActiveBySection(ConstantHelpers.ENTITIES.SECTION_ITEMS.TYPES.SERVICES);
            var result = services.Select(x => new SectionItemViewModel
            {
                Description = x.Description,
                Headline = x.HeadLine,
                UrlImage = x.UrlImage
            }).ToList();
            var view = await this.RenderViewToStringAsync(@"\Views\Portal\Partials\_Services.cshtml", result);
            return Ok(view);
        }
        [HttpGet("get-banner-abogado")]
        public async Task<IActionResult> GetLawyerBanner()
        {
            var services = await _sectionItemService.GetActiveBySection(ConstantHelpers.ENTITIES.SECTION_ITEMS.TYPES.LAWYER_BANNER);
            var result = services.Select(x => new SectionItemViewModel
            {
                Description = x.Description,
                Headline = x.HeadLine,
                UrlImage = x.UrlImage
            }).ToList();
            var view = await this.RenderViewToStringAsync(@"\Views\Portal\Partials\_LawyerBanner.cshtml", result);
            return Ok(view);
        }
        [HttpGet("get-como-funciona-cliente")]
        public async Task<IActionResult> GetHowItWorksClient()
        {
            var howItWorks = await _howItWorksStepService.GetAllActive(ConstantHelpers.ENTITIES.HOW_IT_WORKS.TYPE.CLIENT);
            var view = await this.RenderViewToStringAsync(@"\Views\Portal\Partials\_HowItWorksClientSteps.cshtml", howItWorks);
            return Ok(view);
        }
        [HttpGet("get-como-funciona-resumen-cliente")]
        public async Task<IActionResult> GetHowItWorksSummaryClient()
        {
            var lawyersprofiles = await _sectionItemService.GetActiveBySection(ConstantHelpers.ENTITIES.SECTION_ITEMS.TYPES.CLIENT__HIW);
            var lpvm = lawyersprofiles.Select(x => new SectionItemViewModel
            {
                Description = x.Description,
                Headline = x.HeadLine,
                UrlImage = x.UrlImage
            }).ToList();
            var view = await this.RenderViewToStringAsync(@"\Views\Portal\Partials\_HowItWorksSummaryClientSteps.cshtml", lpvm);
            return Ok(view);
        }
        [HttpGet("get-como-funciona-abogado")]
        public async Task<IActionResult> GetHowItWorksLawyer()
        {
            var howItWorks = await _howItWorksStepService.GetAllActive(ConstantHelpers.ENTITIES.HOW_IT_WORKS.TYPE.LAWYER);
            var view = await this.RenderViewToStringAsync(@"\Views\Portal\Partials\_HowItWorksLawyerSteps.cshtml", howItWorks);
            return Ok(view);
        }
        [HttpGet("get-como-funciona-resumen-abogado")]
        public async Task<IActionResult> GetHowItWorksSummaryLawyer()
        {
            var lawyersprofiles = await _sectionItemService.GetActiveBySection(ConstantHelpers.ENTITIES.SECTION_ITEMS.TYPES.LAWYER_HIW);
            var lpvm = lawyersprofiles.Select(x => new SectionItemViewModel
            {
                Description = x.Description,
                Headline = x.HeadLine,
                UrlImage = x.UrlImage
            }).ToList();
            var view = await this.RenderViewToStringAsync(@"\Views\Portal\Partials\_HowItWorksSummaryLawyerSteps.cshtml", lpvm);
            return Ok(view);
        }
        [HttpGet("get-mision-vision")]
        public async Task<IActionResult> GetMission()
        {
            var misionvision = await _missionVisionService.GetMissionVision();
            var view = await this.RenderViewToStringAsync(@"\Views\Portal\Partials\_MissionVision.cshtml", misionvision);
            return Ok(view);
        }
        [HttpGet("get-nuestro-equipo")]
        public async Task<IActionResult> GetOurTeam()
        {
            var team = await _sectionItemService.GetActiveBySection(ConstantHelpers.ENTITIES.SECTION_ITEMS.TYPES.OUR_TEAM);
            var model = team.Select(x => new SectionItemViewModel
            {
                Description = x.Description,
                Headline = x.HeadLine,
                UrlImage = x.UrlImage
            }).ToList();
            var view = await this.RenderViewToStringAsync(@"\Views\Portal\Partials\_OurTeam.cshtml", model);
            return Ok(view);
        }

        [HttpGet("get-preguntas-frecuentes-abogados")]
        public async Task<IActionResult> GetFrequentQuestions()
        {
            var fq = await _frequentQuestionService.GetAllActive(ConstantHelpers.ENTITIES.FREQUENT_QUESTION.TYPES.FOR_LAWYERS);
            var view = await this.RenderViewToStringAsync(@"\Views\Portal\Partials\_FrequentQuestions.cshtml", fq);
            return Ok(view);
        }
        [HttpGet("get-preguntas-frecuentes-clientes")]
        public async Task<IActionResult> GetFrequentQuestionsClient()
        {
            var fq = await _frequentQuestionService.GetAllActive(ConstantHelpers.ENTITIES.FREQUENT_QUESTION.TYPES.FOR_CLIENTS);
            var view = await this.RenderViewToStringAsync(@"\Views\Portal\Partials\_FrequentQuestions.cshtml", fq);
            return Ok(view);
        }

        //Cosas Generales
        [HttpGet("terminos-condiciones")]
        public IActionResult TermAndConditions()
            => View();

        [HttpGet("politica-privacidad")]
        public IActionResult PolicyAndPrivacy()
            => View();
    }
}
