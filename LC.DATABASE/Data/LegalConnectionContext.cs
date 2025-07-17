using LC.CORE.Helpers;
using LC.ENTITIES.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace LC.DATABASE.Data
{
    public class LegalConnectionContext : IdentityDbContext<ApplicationUser, ApplicationRole, string, IdentityUserClaim<string>, ApplicationUserRole, IdentityUserLogin<string>, IdentityRoleClaim<string>, IdentityUserToken<string>>
    {
        public LegalConnectionContext(DbContextOptions<LegalConnectionContext> options) : base(options)
        {
            Database.SetCommandTimeout(180);
        }

        #region TABLES
        public DbSet<Configuration> Configurations { get; set; }
        public DbSet<Client> Clients { get; set; }
        public DbSet<Department> Departments { get; set; }
        public DbSet<District> Districts { get; set; }
        public DbSet<Province> Provinces { get; set; }
        public DbSet<Lawyer> Lawyers { get; set; }
        public DbSet<LawyerSpecialityTheme> LawyerSpecialityThemes { get; set; }
        public DbSet<LawyerExperience> LawyerExperiences { get; set; }
        public DbSet<LawyerPublication> LawyerPublications { get; set; }
        public DbSet<LawyerLanguage> LawyerLanguages { get; set; }
        public DbSet<LawyerStudy> LawyerStudies { get; set; }
        public DbSet<Speciality> Specialities { get; set; }
        public DbSet<Language> Languages { get; set; }
        public DbSet<LegalCase> LegalCases { get; set; }
        public DbSet<LegalCaseApplicantLawyer> LegalCaseApplicantLawyers { get; set; }
        public DbSet<SpecialityTheme> SpecialityThemes { get; set; }
        public DbSet<LegalCaseLawyer> LegalCaseLawyers { get; set; }
        public DbSet<Banner> Banners { get; set; }
        public DbSet<Payment> Payments { get; set; }
        public DbSet<LawyerObservation> LawyerObservations { get; set; }
        public DbSet<LegalCaseFiledLawyer> LegalCaseFiledLawyers { get; set; }
        public DbSet<SectionItem> SectionItems { get; set; }
        public DbSet<MissionVision> MissionVisions { get; set; }
        public DbSet<HowItWorksStep> HowItWorksSteps { get; set; }
        public DbSet<FrequentQuestion> FrequentQuestions { get; set; }
        public DbSet<Shortcut> Shortcuts { get; set; }
        public DbSet<SocialNetwork> SocialNetworks { get; set; }
        public DbSet<Notification> Notifications { get; set; }
        public DbSet<UserNotification> UserNotifications { get; set; }
        public DbSet<LawyerQualification> LawyerQualifications { get; set; }
        public DbSet<LegalCaseResponse> LegalCaseResponses { get; set; }
        public DbSet<LegalCaseQuestion> LegalCaseQuestions { get; set; }
        public DbSet<Blog> Blogs { get; set; }
        public DbSet<ExternalPublication> ExternalPublications { get; set; }
        public DbSet<LegalCaseObservation> LegalCaseObservations { get; set; }
        public DbSet<Plan> Plans { get; set; }
        public DbSet<PlanBenefit> PlanBenefits { get; set; }
        public DbSet<Benefit> Benefits { get; set; }
        public DbSet<LawyerPlanHistory> LawyerPlanHistories { get; set; }
        public DbSet<LawyerPlanDetail> LawyerPlanDetails { get; set; }
        public DbSet<LawyerWithdrawalInfo> LawyerWithdrawalInfos { get; set; }
        public DbSet<LawyerWithdrawalRequest> WithdrawalRequests { get; set; }
        public DbSet<LawyerCard> LawyerCards { get; set; }
        public DbSet<LawyerInterview> LawyerInterviews { get; set; }
        public DbSet<LegalCaseSpecialityTheme> LegalCaseSpecialityThemes { get; set; }
        public DbSet<LawyerWithdrawal> LawyerWithdrawals { get; set; }
        public DbSet<TemporalLawyerStudy> TemporalLawyerStudies { get; set; }
        public DbSet<TemporalLawyerSpecialityTheme> TemporalLawyerSpecialityThemes { get; set; }
        public DbSet<TemporalLawyerLanguage> TemporalLawyerLanguages { get; set; }
        public DbSet<TemporalLawyerExperience> TemporalLawyerExperiences { get; set; }
        public DbSet<TemporalLawyer> TemporalLawyers { get; set; }
        public DbSet<LegalCaseDelayedTask> LegalCaseDelayedTasks { get; set; }

        #endregion

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var relationship in modelBuilder.Model.GetEntityTypes().SelectMany(e => e.GetForeignKeys()))
            {
                relationship.DeleteBehavior = DeleteBehavior.Restrict;
            }

            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<ApplicationUser>(x =>
            {
                x.Property(r => r.Sex).HasDefaultValue(ConstantHelpers.ENTITIES.USER.SEX.UNSPECIFIED);
                x.Property(r => r.RegisterBy).HasDefaultValue(ConstantHelpers.ENTITIES.USER.REGISTER_BY.LC);
            });

            modelBuilder.Entity<ApplicationUserRole>(x =>
            {
                x.HasKey(ur => new { ur.UserId, ur.RoleId });
                x.HasOne(ur => ur.Role)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.RoleId)
                    .IsRequired();
                x.HasOne(ur => ur.User)
                    .WithMany(r => r.UserRoles)
                    .HasForeignKey(ur => ur.UserId)
                    .IsRequired();
            });

            modelBuilder.Entity<Configuration>(x => x.ToTable("Configurations"));
            modelBuilder.Entity<Client>(x => x.ToTable("Clients"));
            modelBuilder.Entity<Department>(x => x.ToTable("Departments"));
            modelBuilder.Entity<District>(x => x.ToTable("Districts"));
            modelBuilder.Entity<Province>(x => x.ToTable("Provinces"));
            modelBuilder.Entity<Lawyer>(x =>
            {
                x.Property(r => r.PublicProfile).HasDefaultValue(true);
                x.Property(r => r.FreeUser).HasDefaultValue(true);
                x.ToTable("Lawyers");
            });
            modelBuilder.Entity<LawyerSpecialityTheme>(x => x.ToTable("LawyerSpecialityThemes"));
            modelBuilder.Entity<LawyerExperience>(x =>
            {
                x.Property(r => r.TemporalStatus).HasDefaultValue(ConstantHelpers.ENTITIES.LAWYER_EXPERIENCE.TEMPORAL_STATUS.VALIDATED);
                x.ToTable("LawyerExperiences");
            });
            modelBuilder.Entity<LawyerPublication>(x => x.ToTable("LawyerPublications"));
            modelBuilder.Entity<LawyerStudy>(x => x.ToTable("LawyerStudies"));
            modelBuilder.Entity<Speciality>(x => x.ToTable("Specialities"));
            modelBuilder.Entity<Language>(x => x.ToTable("Languages"));
            modelBuilder.Entity<LawyerLanguage>(x => x.ToTable("LawyerLanguages"));
            modelBuilder.Entity<LegalCase>(x => x.ToTable("LegalCases"));
            modelBuilder.Entity<LegalCaseApplicantLawyer>(x =>
            {
                x.HasKey(t => new { t.LawyerId, t.LegalCaseId });
                x.ToTable("LegalCaseApplicantLawyers");
            });

            modelBuilder.Entity<SpecialityTheme>(x => x.ToTable("SpecialityThemes"));

            modelBuilder.Entity<LegalCaseLawyer>(x =>
            {
                x.HasKey(t => new { t.LawyerId, t.LegalCaseId });
                x.ToTable("LegalCaseLawyers");
            });
            modelBuilder.Entity<Banner>(x => x.ToTable("Banners"));
            modelBuilder.Entity<Payment>(x => x.ToTable("Payments"));
            modelBuilder.Entity<LawyerObservation>(x => x.ToTable("LawyerObservations"));
            modelBuilder.Entity<LegalCaseFiledLawyer>(x =>
            {
                x.HasKey(t => new { t.LawyerId, t.LegalCaseId });
                x.ToTable("LegalCaseFiledLawyers");
            });
            modelBuilder.Entity<SectionItem>(x => x.ToTable("SectionItems"));
            modelBuilder.Entity<LawyerQualification>(x =>
            {
                x.HasKey(t => new { t.LawyerId, t.LegalCaseId, t.ClientId });
                x.ToTable("LawyerQualifications");
            });
            modelBuilder.Entity<LegalCaseQuestion>(x => x.ToTable("LegalCaseQuestions"));
            modelBuilder.Entity<LegalCaseResponse>(x => x.ToTable("LegalCaseResponses"));

            modelBuilder.Entity<MissionVision>(x => x.ToTable("MissionVisions"));
            modelBuilder.Entity<FrequentQuestion>(x => x.ToTable("FrequentQuestions"));
            modelBuilder.Entity<Shortcut>(x => x.ToTable("Shortcuts"));
            modelBuilder.Entity<SocialNetwork>(x => x.ToTable("SocialNetworks"));
            modelBuilder.Entity<Blog>(x => x.ToTable("Blogs"));
            modelBuilder.Entity<ExternalPublication>(x => x.ToTable("ExternalPublications"));
            modelBuilder.Entity<LegalCaseObservation>(x => x.ToTable("LegalCaseObservations"));
            modelBuilder.Entity<LawyerPlanHistory>(x => x.ToTable("LawyerPlanHistories"));
            modelBuilder.Entity<LawyerPlanDetail>(x =>
            {
                x.HasKey(t => new { t.LawyerId });
                x.ToTable("LawyerPlanDetails");
            });

            modelBuilder.Entity<Plan>(x => x.ToTable("Plans"));
            modelBuilder.Entity<Benefit>(x => x.ToTable("Benefits"));
            modelBuilder.Entity<PlanBenefit>(x =>
            {
                x.HasKey(t => new { t.PlanId, t.BenefitId });
                x.ToTable("PlanBenefits");
            });

            modelBuilder.Entity<LawyerWithdrawalRequest>(x =>
            {
                x.Property(r => r.Status).HasDefaultValue(ConstantHelpers.ENTITIES.WITHDRAWAL_REQUEST.STATUS.IN_PROCESS);
                x.ToTable("LawyerWithdrawalRequests");
            });
            modelBuilder.Entity<LawyerWithdrawalInfo>(x =>
            {
                x.Property(r => r.FinancialEntity).HasDefaultValue(ConstantHelpers.ENTITIES.LAWYER_WITHDRAWAL_INFO.FINANCIAL_ENTITY.NONE);
                x.ToTable("LawyerWithdrawalInfos");
            });
            modelBuilder.Entity<LawyerCard>(x => x.ToTable("LawyerCards"));
            modelBuilder.Entity<LawyerInterview>(x => x.ToTable("LawyerInterviews"));
            modelBuilder.Entity<LegalCaseSpecialityTheme>(x =>
            {
                x.HasKey(t => new { t.LegalCaseId, t.SpecialityThemeId });
                x.ToTable("LegalCaseSpecialityThemes");
            });
            modelBuilder.Entity<LawyerWithdrawal>(x => x.ToTable("LawyerWithdrawals"));

            modelBuilder.Entity<TemporalLawyerStudy>(x =>
            {
                x.HasKey(t => t.LawyerStudyId);
                x.ToTable("TemporalLawyerStudies");
            });
            modelBuilder.Entity<TemporalLawyerSpecialityTheme>(x => x.ToTable("TemporalLawyerSpecialityThemes"));
            modelBuilder.Entity<TemporalLawyerLanguage>(x =>
            {
                x.HasKey(t => t.LawyerLanguageId);
                x.ToTable("TemporalLawyerLanguages");
            });
            modelBuilder.Entity<TemporalLawyerExperience>(x =>
            {
                x.HasKey(t => t.LawyerExperienceId);
                x.ToTable("TemporalLawyerExperiences");
            });
            modelBuilder.Entity<TemporalLawyer>(x => x.ToTable("TemporalLawyers"));

            modelBuilder.Entity<LegalCaseDelayedTask>(x =>
            {
                x.HasKey(t => new { t.LegalCaseId, t.Task});
                x.ToTable("LegalCaseDelayedTasks");
            });
        }
    }
}
