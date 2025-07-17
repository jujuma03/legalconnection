using LC.CORE.Helpers;
using LC.DATABASE.Data;
using LC.ENTITIES.Custom;
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
    public class LawyerSpecialityThemeRepository : Repository<LawyerSpecialityTheme>, ILawyerSpecialityThemeRepository
    {
        public LawyerSpecialityThemeRepository(LegalConnectionContext context) : base(context) { }

        public async Task<IEnumerable<LawyerSpecialityTheme>> GetSpecialitiesByLawyer(Guid lawyerId)
            => await _context.LawyerSpecialityThemes.Include(x => x.SpecialityTheme.Speciality).Where(x => x.LawyerId == lawyerId).ToArrayAsync();

        public async Task<ResultCustomModel> ValidateSpecialityTheme(List<Guid> specialityThemeIds)
        {
            var result = new ResultCustomModel();

            var confiMaxSpecialities = await _context.Configurations.Where(x => x.Key == ConstantHelpers.CONFIGURATION.MAX_SPECIALITY).FirstOrDefaultAsync();
            var confiMaxThemesBySpeciality = await _context.Configurations.Where(x => x.Key == ConstantHelpers.CONFIGURATION.MAX_THEME_BY_SPECIALITY).FirstOrDefaultAsync();

            if (confiMaxSpecialities == null)
            {
                confiMaxSpecialities = new Configuration
                {
                    Key = ConstantHelpers.CONFIGURATION.MAX_SPECIALITY,
                    Value = ConstantHelpers.CONFIGURATION.DEFAULT_VALUES[ConstantHelpers.CONFIGURATION.MAX_SPECIALITY]
                };
            }

            if (confiMaxThemesBySpeciality == null)
            {
                confiMaxThemesBySpeciality = new Configuration
                {
                    Key = ConstantHelpers.CONFIGURATION.MAX_THEME_BY_SPECIALITY,
                    Value = ConstantHelpers.CONFIGURATION.DEFAULT_VALUES[ConstantHelpers.CONFIGURATION.MAX_THEME_BY_SPECIALITY]
                };
            }

            var maxSpecialities = Convert.ToInt32(confiMaxSpecialities.Value);
            var maxThemesBySpeciality = Convert.ToInt32(confiMaxThemesBySpeciality.Value);
            var maxThemes = maxSpecialities * maxThemesBySpeciality;

            var specialities = await _context.SpecialityThemes
                .Select(x => new
                {
                    x.Id,
                    x.SpecialityId,
                    speciality = x.Speciality.OfficialName,
                    name = x.OfficialName
                })
                .Where(x => specialityThemeIds.Contains(x.Id)).ToListAsync();

            var countSpecialities = specialities.GroupBy(x => x.SpecialityId)
                .Select(x => new { count = x.Count(), name = x.Select(y => y.speciality).FirstOrDefault() })
                .ToList();
            if (maxSpecialities!=0)
            {
                if (countSpecialities.Count() > maxSpecialities)
                {
                    result.Message = $"Solo se pueden seleccionar {maxSpecialities} especialidades";
                    return result;
                }

                if (countSpecialities.Any(y => y.count > maxThemesBySpeciality))
                {
                    var speciality = countSpecialities.Where(y => y.count > maxThemesBySpeciality).FirstOrDefault();
                    result.Message = $"Se han seleccionado {speciality.count} temas de la especialidad {speciality.name}, sin embargo solo se pueden seleccionar {maxThemesBySpeciality} temas.";
                    return result;
                }
            }

            var totalThemes = countSpecialities.Select(x => x.count).Sum();
            if (maxThemes>0)
            {
                if (totalThemes > maxThemes)
                {
                    result.Message = $"Se puede seleccionar como máximo {maxThemes} temas.";
                    return result;
                }
            }

            result.Success = true;
            return result;
        }

        public async Task<bool> AnyByLawyer(Guid lawyerId)
            => await _context.LawyerSpecialityThemes.Where(x => x.LawyerId == lawyerId).AnyAsync();
    }
}
