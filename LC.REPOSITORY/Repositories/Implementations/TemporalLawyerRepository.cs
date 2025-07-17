using LC.CORE.Helpers;
using LC.CORE.Services;
using LC.DATABASE.Data;
using LC.ENTITIES.Models;
using LC.REPOSITORY.Base;
using LC.REPOSITORY.Repositories.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LC.REPOSITORY.Repositories.Implementations
{
    public class TemporalLawyerRepository : Repository<TemporalLawyer>, ITemporalLawyerRepository
    {
        private readonly ICloudStorageService _cloudStorageService;

        public TemporalLawyerRepository(
            LegalConnectionContext context,
            ICloudStorageService cloudStorageService
            ) : base(context) {
            _cloudStorageService = cloudStorageService;
        }

        public async Task<TemporalLawyer> GetTemporalLawyer(Guid lawyerId)
        {
            var entity = await _context.TemporalLawyers.Where(x => x.LawyerId == lawyerId).FirstOrDefaultAsync();

            if (entity is null)
            {
                var lawyer = await _context.Lawyers.Where(x => x.Id == lawyerId).Include(x => x.User).FirstOrDefaultAsync();

                entity = new TemporalLawyer
                {
                    AboutMe = lawyer.AboutMe,
                    BirthDate = lawyer.User.BirthDate,
                    CAL = lawyer.CAL,
                    DistrictId = lawyer.User.DistrictId,
                    Document = lawyer.User.Document,
                    DocumentType = lawyer.User.DocumentType,
                    Fee = lawyer.Fee,
                    FreeFirst = lawyer.FreeFirst,
                    HouseNumber = lawyer.User.HouseNumber,
                    LawyerId = lawyer.Id,
                    PhoneNumber = lawyer.User.PhoneNumber,
                    Name = lawyer.User.Name,
                    Picture = lawyer.User.Picture,
                    Surnames = lawyer.User.Surnames,
                    Sex = lawyer.User.Sex
                };

                await _context.TemporalLawyers.AddAsync(entity);
                await _context.SaveChangesAsync();
            }

            return entity;
        }
        public async Task SaveTemporalLawyer(TemporalLawyer entity)
        {
            var lawyer = await _context.Lawyers.Where(x => x.Id == entity.LawyerId).Include(x => x.User).FirstOrDefaultAsync();
            lawyer.ProfileWithChanges = true;
            await _context.SaveChangesAsync();
        }
        public async Task AcceptProfileChanges(Guid lawyerId)
        {
            var lawyer = await _context.Lawyers.Where(x => x.Id == lawyerId).FirstOrDefaultAsync();
            var user = await _context.Users.Where(x => x.Id == lawyer.UserId).FirstOrDefaultAsync();

            var temporalLawyer = await _context.TemporalLawyers.Where(x => x.LawyerId == lawyerId).FirstOrDefaultAsync();

            if(temporalLawyer != null)
            {
                lawyer.CAL = temporalLawyer.CAL;
                lawyer.Fee = temporalLawyer.Fee;
                lawyer.FreeFirst = temporalLawyer.FreeFirst;
                lawyer.AboutMe = temporalLawyer.AboutMe;
                user.Name = temporalLawyer.Name;
                user.Surnames = temporalLawyer.Surnames;
                user.Document = temporalLawyer.Document;
                user.HouseNumber = temporalLawyer.HouseNumber;
                user.PhoneNumber = temporalLawyer.PhoneNumber;
                user.BirthDate = temporalLawyer.BirthDate;
                user.Sex = temporalLawyer.Sex;
                user.Picture = temporalLawyer.Picture;
                user.DistrictId = temporalLawyer.DistrictId;
                user.DocumentType = temporalLawyer.DocumentType;

                _context.TemporalLawyers.Remove(temporalLawyer);
            }

            //Experiences

            var lawyerExperiences = await _context.LawyerExperiences.Where(x => x.LawyerId == lawyer.Id).ToListAsync();

            foreach (var experience in lawyerExperiences)
            {
                var temporalLawyerExperience = await _context.TemporalLawyerExperiences.Where(x => x.LawyerExperienceId == experience.Id).FirstOrDefaultAsync();

                if (experience.TemporalStatus == ConstantHelpers.ENTITIES.LAWYER_EXPERIENCE.TEMPORAL_STATUS.DELETED)
                {
                    if (!string.IsNullOrEmpty(experience.PhotoUrl))
                        await _cloudStorageService.TryDelete(experience.PhotoUrl, ConstantHelpers.CLOUD_CONTAINERS.LAWYER_EXPERIENCES);

                    if (temporalLawyerExperience != null)
                        _context.TemporalLawyerExperiences.Remove(temporalLawyerExperience);

                    _context.LawyerExperiences.Remove(experience);
                }
                else if (experience.TemporalStatus == ConstantHelpers.ENTITIES.LAWYER_EXPERIENCE.TEMPORAL_STATUS.UPDATED)
                {
                    if(temporalLawyerExperience != null)
                    {
                        experience.Company = temporalLawyerExperience.Company;
                        experience.Description = temporalLawyerExperience.Description;
                        experience.EndDate = temporalLawyerExperience.EndDate;
                        experience.PhotoUrl = temporalLawyerExperience.PhotoUrl;
                        experience.Position = temporalLawyerExperience.Position;
                        experience.StartDate = temporalLawyerExperience.StartDate;
                        experience.WorkArea = temporalLawyerExperience.WorkArea;
                    }
                }

                experience.TemporalStatus = ConstantHelpers.ENTITIES.LAWYER_EXPERIENCE.TEMPORAL_STATUS.VALIDATED;
            }

            // Studies

            var lawyerStudies = await _context.LawyerStudies.Where(x => x.LawyerId == lawyer.Id).ToListAsync();

            foreach (var study in lawyerStudies)
            {
                var temporalLawyerStudy = await _context.TemporalLawyerStudies.Where(x => x.LawyerStudyId == study.Id).FirstOrDefaultAsync();

                if(study.TemporalStatus == ConstantHelpers.ENTITIES.LAWYER_STUDY.TEMPORAL_STATUS.DELETED)
                {
                    if (temporalLawyerStudy != null)
                        _context.TemporalLawyerStudies.Remove(temporalLawyerStudy);

                    _context.LawyerStudies.Remove(study);
                }
                else if (study.TemporalStatus == ConstantHelpers.ENTITIES.LAWYER_STUDY.TEMPORAL_STATUS.UPDATED)
                {
                    if(temporalLawyerStudy != null)
                    {
                        study.Description = temporalLawyerStudy.Description;
                        study.EndDate = temporalLawyerStudy.EndDate;
                        study.Grade = temporalLawyerStudy.Grade;
                        study.Mention = temporalLawyerStudy.Mention;
                        study.PhotoUrl = temporalLawyerStudy.PhotoUrl;
                        study.StartDate = temporalLawyerStudy.StartDate;
                        study.Ubication = temporalLawyerStudy.Ubication;
                    }
                }

                study.TemporalStatus = ConstantHelpers.ENTITIES.LAWYER_STUDY.TEMPORAL_STATUS.VALIDATED;
            }

            //Languages

            var lawyerLanguage = await _context.LawyerLanguages.Where(x => x.LawyerId == lawyer.Id).ToListAsync();

            foreach (var language in lawyerLanguage)
            {
                var temporalLawyerLanguage = await _context.TemporalLawyerLanguages.Where(x => x.LawyerLanguageId == language.Id).FirstOrDefaultAsync();

                if(language.TemporalStatus == ConstantHelpers.ENTITIES.LAWYER_LANGUAGE.TEMPORAL_STATUS.DELETED)
                {
                    if (temporalLawyerLanguage != null)
                        _context.TemporalLawyerLanguages.Remove(temporalLawyerLanguage);

                    _context.LawyerLanguages.Remove(language);
                }

                language.TemporalStatus = ConstantHelpers.ENTITIES.LAWYER_LANGUAGE.TEMPORAL_STATUS.VALIDATED;
            }

            lawyer.ProfileWithChanges = false;
            await _context.SaveChangesAsync();

        }
        public async Task RejectProfileChanges(Guid lawyerId)
        {
            var lawyer = await _context.Lawyers.Where(x => x.Id == lawyerId).FirstOrDefaultAsync();
            var temporalLawyer = await _context.TemporalLawyers.Where(x => x.LawyerId == lawyerId).FirstOrDefaultAsync();

            lawyer.ProfileWithChanges = false;
            _context.TemporalLawyers.Remove(temporalLawyer);

            //Experiences
            var lawyerExperiences = await _context.LawyerExperiences.Where(x => x.LawyerId == lawyer.Id).ToListAsync();
            _context.LawyerExperiences.RemoveRange(lawyerExperiences.Where(x => x.TemporalStatus == ConstantHelpers.ENTITIES.LAWYER_EXPERIENCE.TEMPORAL_STATUS.NEW).ToList());
            lawyerExperiences.Where(x => x.TemporalStatus != ConstantHelpers.ENTITIES.LAWYER_EXPERIENCE.TEMPORAL_STATUS.NEW).ToList().ForEach(x => x.TemporalStatus = ConstantHelpers.ENTITIES.LAWYER_EXPERIENCE.TEMPORAL_STATUS.VALIDATED);

            //Studies
            var lawyerStudies = await _context.LawyerStudies.Where(x => x.LawyerId == lawyer.Id).ToListAsync();
            _context.LawyerStudies.RemoveRange(lawyerStudies.Where(x => x.TemporalStatus == ConstantHelpers.ENTITIES.LAWYER_LANGUAGE.TEMPORAL_STATUS.NEW).ToList());
            lawyerStudies.Where(x => x.TemporalStatus != ConstantHelpers.ENTITIES.LAWYER_LANGUAGE.TEMPORAL_STATUS.NEW).ToList().ForEach(x => x.TemporalStatus = ConstantHelpers.ENTITIES.LAWYER_STUDY.TEMPORAL_STATUS.VALIDATED);

            //Languages
            var lawyerLanguages = await _context.LawyerLanguages.Where(x => x.LawyerId == lawyer.Id).ToListAsync();
            _context.LawyerLanguages.RemoveRange(lawyerLanguages.Where(x => x.TemporalStatus == ConstantHelpers.ENTITIES.LAWYER_LANGUAGE.TEMPORAL_STATUS.NEW).ToList());
            lawyerLanguages.Where(x => x.TemporalStatus != ConstantHelpers.ENTITIES.LAWYER_LANGUAGE.TEMPORAL_STATUS.NEW).ToList().ForEach(x => x.TemporalStatus = ConstantHelpers.ENTITIES.LAWYER_LANGUAGE.TEMPORAL_STATUS.VALIDATED);


            await _context.SaveChangesAsync();
        }

        #region -- EXPERIENCE --

        public async Task<TemporalLawyerExperience> GetTemporalLawyerExperience(Guid lawyerExperienceId)
        {
            var lawyerExperience = await _context.LawyerExperiences.Where(x => x.Id == lawyerExperienceId).FirstOrDefaultAsync();
            var temporalLawyerExperience = await _context.TemporalLawyerExperiences.Where(x => x.LawyerExperienceId == lawyerExperienceId).FirstOrDefaultAsync();
            if (temporalLawyerExperience is null)
            {
                temporalLawyerExperience = new TemporalLawyerExperience
                {
                    LawyerExperienceId = lawyerExperienceId,
                    LawyerId = lawyerExperience.LawyerId
                };

                await _context.TemporalLawyerExperiences.AddAsync(temporalLawyerExperience);
                await _context.SaveChangesAsync();
            }

            return temporalLawyerExperience;
        }

        #endregion

        #region -- STUDY --

        public async Task<TemporalLawyerStudy> GetTemporalLawyerStudy(Guid lawyerStudyId)
        {
            var lawyerStudy = await _context.LawyerStudies.Where(x => x.Id == lawyerStudyId).FirstOrDefaultAsync();
            var temporalLawyerStudy = await _context.TemporalLawyerStudies.Where(x => x.LawyerStudyId == lawyerStudyId).FirstOrDefaultAsync();
            if (temporalLawyerStudy is null)
            {
                temporalLawyerStudy = new TemporalLawyerStudy
                {
                    LawyerStudyId = lawyerStudyId,
                    LawyerId = lawyerStudy.LawyerId
                };

                await _context.TemporalLawyerStudies.AddAsync(temporalLawyerStudy);
                await _context.SaveChangesAsync();
            }

            return temporalLawyerStudy;
        }

        #endregion

        #region -- LANGUAGE --

        public async Task<TemporalLawyerLanguage> GetTemporalLawyerLanguage(Guid lawyerLanguageId)
        {
            var lawyerStudy = await _context.LawyerLanguages.Where(x => x.Id == lawyerLanguageId).FirstOrDefaultAsync();
            var temporalLawyerLanguage = await _context.TemporalLawyerLanguages.Where(x => x.LawyerLanguageId == lawyerLanguageId).FirstOrDefaultAsync();
            if (temporalLawyerLanguage is null)
            {
                temporalLawyerLanguage = new TemporalLawyerLanguage
                {
                    LawyerLanguageId = lawyerLanguageId,
                    LawyerId = temporalLawyerLanguage.LawyerId
                };

                await _context.TemporalLawyerLanguages.AddAsync(temporalLawyerLanguage);
                await _context.SaveChangesAsync();
            }

            return temporalLawyerLanguage;
        }

        #endregion
    }
}
