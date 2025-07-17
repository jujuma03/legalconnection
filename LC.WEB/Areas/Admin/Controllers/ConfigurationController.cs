using LC.CORE.Helpers;
using LC.SERVICE.Services.Interfaces;
using LC.WEB.Areas.Admin.Models.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.WEB.Areas.Admin.Controllers
{
    [Authorize(Roles = ConstantHelpers.ROLES.ADMIN)]
    [Area("Admin")]
    [Route("admin/configuracion")]
    public class ConfigurationController : Controller
    {
        private readonly IConfigurationService _configurationService;

        public ConfigurationController(
            IConfigurationService configurationService
            )
        {
            _configurationService = configurationService;
        }

        public async Task<IActionResult> Index()
        {
            var max_vacancies = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_VACANCIES);
            var max_speciality = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_SPECIALITY);
            var max_theme_by_speciality = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_THEME_BY_SPECIALITY);
            var new_lawyer_validation_process_days = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.NEW_LAWYER_MAX_HOUR_TIME_VALIDATION_PROCESS);
            var max_hour_time_filed_legal_case = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_FILED_LEGAL_CASE);
            var max_hour_time_to_lawyer_postulate = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_TO_LAWYER_POSTULATE);
            var max_hour_time_to_client_accept_pay_lawyer = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_TO_CLIENT_ACCEPT_AND_PAY_LAWYER);
            var max_length_description_legal_case = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_LENGTH_DESCRIPTION_LEGAL_CASE);
            var withdrawal_request_day = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.WITHDRAWAL_REQUEST_DAY);
            var work_schedule_start = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.WORK_SCHEDULE_START);
            var work_schedule_end = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.WORK_SCHEDULE_END);
            var free_consultants = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.FREE_CONSULTING);

            var max_hour_time_to_client_pay_lawyer = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_TO_CLIENT_PAY_LAWYER);
            var max_hour_time_to_lawyer_accept_direct = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_TO_LAWYER_ACCEPT_DIRECT);
            var max_hour_to_send_email_confirmation = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_HOUR_TO_SEND_EMAIL_CONFIRMATION);
            var max_hour_to_close_legal_case = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_HOUR_TO_CLOSE_LEGAL_CASE);


            var model = new ConfigurationViewModel
            {
                MaxSpeciality = Convert.ToInt32(max_speciality.Value),
                MaxThemeBySpeciality = Convert.ToInt32(max_theme_by_speciality.Value),
                MaxVacancies = Convert.ToInt32(max_vacancies.Value),
                NewLawyerMaxHourTimevalidationProcess = Convert.ToInt32(new_lawyer_validation_process_days.Value),
                MaxHourTimeFiledLegalCase = Convert.ToInt32(max_hour_time_filed_legal_case.Value),
                MaxHourTimeToLawyerPostulate = Convert.ToInt32(max_hour_time_to_lawyer_postulate.Value),
                MaxHourTimeToClientAcceptAndPayLawyer = Convert.ToInt32(max_hour_time_to_client_accept_pay_lawyer.Value),
                MaxLengthDescriptionLegalCase = Convert.ToInt32(max_length_description_legal_case.Value),
                WithdrawalRequestDay = withdrawal_request_day.Value,
                WorkScheduleStart = work_schedule_start.Value,
                WorkScheduleEnd = work_schedule_end.Value,
                FreeConsulting = Convert.ToInt32(free_consultants.Value),
                MaxHourTimeToClientPayLawyer = Convert.ToInt32(max_hour_time_to_client_pay_lawyer.Value),
                MaxHourTimeTolawyerAcceptDirect = Convert.ToInt32(max_hour_time_to_lawyer_accept_direct.Value),
                MaxHourToSendEmailConfirmation = Convert.ToInt32(max_hour_to_send_email_confirmation.Value),
                MaxHourToCloseLegalCase = Convert.ToInt32(max_hour_to_close_legal_case.Value)
            };

            return View(model);
        }
        
        [HttpPost("actualizar")]
        public async Task<IActionResult> Update(ConfigurationViewModel configuration)
        {
            var max_vacancies = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_VACANCIES);
            var max_speciality = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_SPECIALITY);
            var max_theme_by_speciality = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_THEME_BY_SPECIALITY);
            var new_lawyer_validation_process_days = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.NEW_LAWYER_MAX_HOUR_TIME_VALIDATION_PROCESS);
            var max_hour_time_filed_legal_case = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_FILED_LEGAL_CASE);
            var max_hour_time_to_lawyer_postulate = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_TO_LAWYER_POSTULATE);
            var max_hour_time_to_client_accept_pay_lawyer = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_TO_CLIENT_ACCEPT_AND_PAY_LAWYER);
            var max_length_description_legal_case = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_LENGTH_DESCRIPTION_LEGAL_CASE);
            var withdrawal_request_day = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.WITHDRAWAL_REQUEST_DAY);
            var work_schedule_start = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.WORK_SCHEDULE_START);
            var work_schedule_end = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.WORK_SCHEDULE_END);
            var free_consulting = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.FREE_CONSULTING);

            var max_hour_time_to_client_pay_lawyer = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_TO_CLIENT_PAY_LAWYER);
            var max_hour_time_to_lawyer_accept_direct = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_TO_LAWYER_ACCEPT_DIRECT);
            var max_hour_to_send_email_confirmation = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_HOUR_TO_SEND_EMAIL_CONFIRMATION);
            var max_hour_to_close_legal_case = await _configurationService.GetByKey(ConstantHelpers.CONFIGURATION.MAX_HOUR_TO_CLOSE_LEGAL_CASE);

            max_vacancies.Value = $"{configuration.MaxVacancies}";
            max_speciality.Value = $"{configuration.MaxSpeciality}";
            max_theme_by_speciality.Value = $"{configuration.MaxThemeBySpeciality}";
            new_lawyer_validation_process_days.Value = $"{configuration.NewLawyerMaxHourTimevalidationProcess}";
            max_hour_time_filed_legal_case.Value = $"{configuration.MaxHourTimeFiledLegalCase}";
            max_hour_time_to_lawyer_postulate.Value = $"{configuration.MaxHourTimeToLawyerPostulate}";
            max_hour_time_to_client_accept_pay_lawyer.Value = $"{configuration.MaxHourTimeToClientAcceptAndPayLawyer}";
            max_length_description_legal_case.Value = $"{configuration.MaxLengthDescriptionLegalCase}";
            withdrawal_request_day.Value = $"{configuration.WithdrawalRequestDay}";
            work_schedule_start.Value =$"{configuration.WorkScheduleStart}";
            work_schedule_end.Value =$"{configuration.WorkScheduleEnd}";
            free_consulting.Value =$"{configuration.FreeConsulting}";

            max_hour_time_to_client_pay_lawyer.Value = $"{configuration.MaxHourTimeToClientPayLawyer}";
            max_hour_time_to_lawyer_accept_direct.Value = $"{configuration.MaxHourTimeTolawyerAcceptDirect}";
            max_hour_to_send_email_confirmation.Value = $"{configuration.MaxHourToSendEmailConfirmation}";
            max_hour_to_close_legal_case.Value = $"{configuration.MaxHourToCloseLegalCase}";

            await _configurationService.Update(max_vacancies);
            return Ok();
        }
    }
}
