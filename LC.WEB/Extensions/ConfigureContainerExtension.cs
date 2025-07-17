using LC.REPOSITORY.Base;
using LC.CORE.Services.Implementations;
using LC.CORE.Services.Interfaces;
using LC.REPOSITORY.Repositories.Implementations;
using LC.REPOSITORY.Repositories.Interfaces;
using LC.SERVICE.Services.Implementations;
using LC.SERVICE.Services.Interfaces;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using LC.ENTITIES.Models;
using LC.WEB.Factories;
using LC.CORE.VIEW.Services.Interfaces;
using LC.CORE.VIEW.Services.Implementations;
using LC.CORE.Services;
using LC.WEB.Hubs.Interfaces;
using LC.WEB.Hubs.Implementations;
using LC.PAYMENT.Services.Culqi;
using LC.PAYMENT.Utilities.RequestManager;
using LC.WEB.Services.Hangfire.Interfaces;
using LC.WEB.Services.Hangfire.Implementations;
using LC.WEB.Services.Google.Interfaces;
using LC.WEB.Services.Google.Implementations;

namespace LC.WEB.Extensions
{
    public static class ConfigureContainerExtension
    {
        public static void AddRepository(this IServiceCollection serviceCollection)
        {
            //Base
            serviceCollection.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            serviceCollection.AddScoped<IUserClaimsPrincipalFactory<ApplicationUser>, ClaimsPrincipalFactory>();

            //Repositories
            serviceCollection.AddScoped(typeof(IUserRepository), typeof(UserRepository));
            serviceCollection.AddScoped(typeof(IClientRepository), typeof(ClientRepository));

            serviceCollection.AddScoped(typeof(IConfigurationRepository), typeof(ConfigurationRepository));
            serviceCollection.AddScoped(typeof(IDistrictRepository), typeof(DistrictRepository));
            serviceCollection.AddScoped(typeof(IDepartmentRepository), typeof(DepartmentRepository));
            serviceCollection.AddScoped(typeof(IProvinceRepository), typeof(ProvinceRepository));
            serviceCollection.AddScoped(typeof(ILawyerRepository), typeof(LawyerRepository));
            serviceCollection.AddScoped(typeof(ILawyerSpecialityThemeRepository), typeof(LawyerSpecialityThemeRepository));
            serviceCollection.AddScoped(typeof(ILawyerExperienceRepository), typeof(LawyerExperienceRepository));
            serviceCollection.AddScoped(typeof(ILawyerStudyRepository), typeof(LawyerStudyRepository));
            serviceCollection.AddScoped(typeof(ILawyerPublicationRepository), typeof(LawyerPublicationRepository));
            serviceCollection.AddScoped(typeof(ILanguageRepository), typeof(LanguageRepository));
            serviceCollection.AddScoped(typeof(ILawyerLanguageRepository), typeof(LawyerLanguageRepository));
            serviceCollection.AddScoped(typeof(ISpecialityRepository), typeof(SpecialityRepository));
            serviceCollection.AddScoped(typeof(ILegalCaseRepository), typeof(LegalCaseRepository));
            serviceCollection.AddScoped(typeof(ISpecialityThemeRepository), typeof(SpecialityThemeRepository));
            serviceCollection.AddScoped(typeof(ILegalCaseLawyerRepository), typeof(LegalCaseLawyerRepository));
            serviceCollection.AddScoped(typeof(IBannerRepository), typeof(BannerRepository));
            serviceCollection.AddScoped(typeof(IPaymentRepository), typeof(PaymentRepository));
            serviceCollection.AddScoped(typeof(ILawyerObservationRepository), typeof(LawyerObservationRepository));
            serviceCollection.AddScoped(typeof(ISectionItemRepository), typeof(SectionItemRepository));
            serviceCollection.AddScoped(typeof(INotificationRepository), typeof(NotificationRepository));
            serviceCollection.AddScoped(typeof(ILawyerQualificationRepository), typeof(LawyerQualificationRepository));
            serviceCollection.AddScoped(typeof(ILegalCaseQuestionRepository), typeof(LegalCaseQuestionRepository));
            serviceCollection.AddScoped(typeof(ILegalCaseResponseRepository), typeof(LegalCaseResponseRepository));
            serviceCollection.AddScoped(typeof(IMissionVisionRepository), typeof(MissionVisionRepository));
            serviceCollection.AddScoped(typeof(IHowItWorksStepRepository), typeof(HowItWorksStepRepository));
            serviceCollection.AddScoped(typeof(IFrequentQuestionRepository), typeof(FrequentQuestionRepository));
            serviceCollection.AddScoped(typeof(ISocialNetworkRepository), typeof(SocialNetworkRepository));
            serviceCollection.AddScoped(typeof(IShortcutRepository), typeof(ShortcutRepository));
            serviceCollection.AddScoped(typeof(IBlogRepository), typeof(BlogRepository));
            serviceCollection.AddScoped(typeof(IExternalPublicationRepository), typeof(ExternalPublicationRepository));
            serviceCollection.AddScoped(typeof(ILegalCaseObservationRepository), typeof(LegalCaseObservationRepository));
            serviceCollection.AddScoped(typeof(IRoleRepository), typeof(RoleRepository));
            serviceCollection.AddScoped(typeof(IBenefitRepository), typeof(BenefitRepository));
            serviceCollection.AddScoped(typeof(IPlanRepository), typeof(PlanRepository));
            serviceCollection.AddScoped(typeof(ILawyerPlanDetailRepository), typeof(LawyerPlanDetailRepository));
            serviceCollection.AddScoped(typeof(ILawyerWithdrawalInfoRepository), typeof(LawyerWithdrawalInfoRepository));
            serviceCollection.AddScoped(typeof(ILawyerWithdrawalRequestRepository), typeof(LawyerWithdrawalRequestRepository));
            serviceCollection.AddScoped(typeof(ILawyerCardRepository), typeof(LawyerCardRepository));
            serviceCollection.AddScoped(typeof(ILawyerInterviewRepository), typeof(LawyerInterviewRepository));
            serviceCollection.AddScoped(typeof(ITemporalLawyerRepository), typeof(TemporalLawyerRepository));
        }

        public static void AddTransientServices(this IServiceCollection serviceCollection)
        {
            //Base
            serviceCollection.AddTransient<IEmailService, EmailService>();
            serviceCollection.AddTransient<IEmailTemplateService, EmailTemplateService>();
            serviceCollection.AddTransient<ISelect2Service, Select2Service>();
            serviceCollection.AddTransient<IDataTableService, DataTableService>();
            serviceCollection.AddTransient<IPaginationService, PaginationService>();
            serviceCollection.AddTransient<IViewRenderService, ViewRenderService>();
            serviceCollection.AddTransient<ICloudStorageService, CloudStorageService>();
            serviceCollection.AddTransient<IPaymentRequestManager, PaymentRequestManager>();
            serviceCollection.AddTransient<ICulqiService, CulqiService>();
            serviceCollection.AddTransient<IHangfireService, HangfireService>();
            serviceCollection.AddTransient<IGoogleService, GoogleService>();
            serviceCollection.AddScoped(typeof(IHubContext), typeof(HubContext));

            //Services
            serviceCollection.AddTransient<IUserService, UserService>();
            serviceCollection.AddTransient<IClientService, ClientService>();

            serviceCollection.AddTransient<IConfigurationService, ConfigurationService>();
            serviceCollection.AddTransient<IDistrictService, DistrictService>();
            serviceCollection.AddTransient<IProvinceService, ProvinceService>();
            serviceCollection.AddTransient<IDepartmentService, DepartmentService>();
            serviceCollection.AddTransient<ILawyerService, LawyerService>();
            serviceCollection.AddTransient<ILawyerSpecialityThemeService, LawyerSpecialityThemeService>();
            serviceCollection.AddTransient<ILawyerExperienceService, LawyerExperienceService>();
            serviceCollection.AddTransient<ILawyerStudyService, LawyerStudyService>();
            serviceCollection.AddTransient<ILawyerPublicationService, LawyerPublicationService>();
            serviceCollection.AddTransient<ILanguageService, LanguageService>();
            serviceCollection.AddTransient<ILawyerLanguageService, LawyerLanguageService>();
            serviceCollection.AddTransient<ISpecialityService, SpecialityService>();
            serviceCollection.AddTransient<ILegalCaseService, LegalCaseService>();
            serviceCollection.AddTransient<ISpecialityThemeService, SpecialityThemeService>();
            serviceCollection.AddTransient<ILegalCaseLawyerService, LegalCaseLawyerService>();
            serviceCollection.AddTransient<IBannerService, BannerService>();
            serviceCollection.AddTransient<IPaymentService, PaymentService>();
            serviceCollection.AddTransient<ILawyerObservationService, LawyerObservationService>();
            serviceCollection.AddTransient<ISectionItemService, SectionItemService>();
            serviceCollection.AddTransient<INotificationService, NotificationService>();
            serviceCollection.AddTransient<ILawyerQualificationService, LawyerQualificationService>();
            serviceCollection.AddTransient<ILegalCaseResponseService, LegalCaseResponseService>();
            serviceCollection.AddTransient<ILegalCaseQuestionService, LegalCaseQuestionService>();
            serviceCollection.AddTransient<IMissionVisionService, MissionVisionService>();
            serviceCollection.AddTransient<IHowItWorksStepService, HowItWorksStepService>();
            serviceCollection.AddTransient<IFrequentQuestionService, FrequentQuestionService>();
            serviceCollection.AddTransient<ISocialNetworkService, SocialNetworkService>();
            serviceCollection.AddTransient<IShortcutService, ShortcutService>();
            serviceCollection.AddTransient<IBlogService, BlogService>();
            serviceCollection.AddTransient<IExternalPublicationService, ExternalPublicationService>();
            serviceCollection.AddTransient<ILegalCaseObservationService, LegalCaseObservationService>();
            serviceCollection.AddTransient<IRoleService, RoleService>();
            serviceCollection.AddTransient<IBenefitService, BenefitService>();
            serviceCollection.AddTransient<IPlanService, PlanService>();
            serviceCollection.AddTransient<ILawyerPlanDetailService, LawyerPlanDetailService>();
            serviceCollection.AddTransient<ILawyerWithdrawalInfoService, LawyerWithdrawalInfoService>();
            serviceCollection.AddTransient<ILawyerWithdrawalRequestService, LawyerWithdrawalRequestService>();
            serviceCollection.AddTransient<ILawyerCardService, LawyerCardService>();
            serviceCollection.AddTransient<ILawyerInterviewService, LawyerInterviewService>();
            serviceCollection.AddTransient<ITemporalLawyerService, TemporalLawyerService>();
        }
    }
}
