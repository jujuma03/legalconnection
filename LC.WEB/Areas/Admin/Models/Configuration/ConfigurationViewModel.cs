using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Models.Configuration
{
    public class ConfigurationViewModel
    {
        public int MaxVacancies { get; set; }
        public int MaxThemeBySpeciality { get; set; }
        public int MaxSpeciality { get; set; }
        public int MaxSpecialityTheme => MaxThemeBySpeciality * MaxSpeciality;
        public int NewLawyerMaxHourTimevalidationProcess { get; set; }
        public int MaxHourTimeFiledLegalCase { get; set; }
        public int MaxHourTimeToLawyerPostulate { get; set; }
        public int MaxHourTimeToClientAcceptAndPayLawyer { get; set; }
        public int MaxLengthDescriptionLegalCase { get; set; }
        public string WithdrawalRequestDay { get; set; }
        public string WorkScheduleStart { get; set; }
        public string WorkScheduleEnd { get; set; }
        public int FreeConsulting { get; set; }
        public int MaxHourTimeToClientPayLawyer { get; set; }
        public int MaxHourTimeTolawyerAcceptDirect { get; set; }
        public int MaxHourToSendEmailConfirmation { get; set; }
        public int MaxHourToCloseLegalCase { get; set; }
    }
}
