using LC.CORE.Helpers;
using LC.CORE.Services.Interfaces;
using LC.CORE.VIEW.Services.Interfaces;
using LC.DATABASE.Data;
using LC.HANGFIRE.Services.DelayedTask.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LC.CORE.VIEW.Services.Models.Email;
using Hangfire;
using LC.CORE.Extensions;
using LC.ENTITIES.Models;

namespace LC.HANGFIRE.Services.DelayedTask.Implementations
{
    public class DelayedTaskLegalCaseService : IDelayedTaskLegalCaseService
    {
        private readonly LegalConnectionContext _context;
        private readonly IEmailService _emailService;
        private readonly IEmailTemplateService _emailTemplateService;

        public DelayedTaskLegalCaseService(
            LegalConnectionContext context,
            IEmailService emailService,
            IEmailTemplateService emailTemplateService
            )
        {
            _context = context;
            _emailService = emailService;
            _emailTemplateService = emailTemplateService;
        }

        public async Task EndApplicationTime(Guid legalCaseId)
        {
            var legalCase = await _context.LegalCases.Where(x => x.Id == legalCaseId).FirstOrDefaultAsync();
            var clientEmail = await _context.Clients.Where(x => x.Id == legalCase.ClientId).Select(x => x.User.Email).FirstOrDefaultAsync();
            var anyApplicant = await _context.LegalCaseApplicantLawyers.Where(x => x.LegalCaseId == legalCaseId).AnyAsync();

            if (anyApplicant)
            {
                var applicants = await _context.LegalCaseApplicantLawyers.Where(x => x.LegalCaseId == legalCaseId).CountAsync();

                legalCase.Status = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.SELECTING_LAWYER;
                legalCase.SelectionLawyerStartDate = DateTime.UtcNow;

                var maxHourToSelectAndPayLawyer = Convert.ToInt32(await _context.Configurations.Where(x => x.Key == ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_TO_CLIENT_ACCEPT_AND_PAY_LAWYER).Select(x => x.Value).FirstOrDefaultAsync());

                var job = BackgroundJob.Schedule(() => EndTimeToSelectAndPaymentLawyer(legalCaseId), DateTime.UtcNow.AddHours(maxHourToSelectAndPayLawyer));

                var legalCaseJob = new LegalCaseDelayedTask
                {
                    Task = ConstantHelpers.ENTITIES.LEGAL_CASE_DELAYED_TASK.TASK.END_TIME_SELECT_AND_PAYMENT_LAWYER,
                    HangfireJobId = job,
                    LegalCaseId = legalCase.Id
                };

                await _context.LegalCaseDelayedTasks.AddAsync(legalCaseJob);
                await _context.SaveChangesAsync();

                //var modelEmail = new StandardEmailModel
                //{
                //    Title = "Nuevos Postulantes",
                //    SubHeader = $"Estimado, se han postulado {applicants} para su caso. Recuerde revisar los perfiles de sus postulantes antes de continuar con el proceso.",
                //    LinkName = $"Ir a {ConstantHelpers.PROJECT}",
                //    LinkRedirect = $"{ConstantHelpers.GENERAL.PROJECT_URI_BASE}/mis-casos/{legalCaseId}/detalle"
                //};
                //var template = await _emailTemplateService.GetStandardEmailTemplate(modelEmail);


                var modelEmail = new LawyerPostulantToCaseEmailModel
                {
                    Title = "Nuevos Postulantes para tu caso",
                    LinkName = $"Ver los postulantes de mi caso",
                    LinkRedirect = $"{ConstantHelpers.GENERAL.PROJECT_URI_BASE}/mis-casos/{legalCaseId}/detalle"
                };

                var template = await _emailTemplateService.GetLawyerPostulantToCaseEmailTemplate(modelEmail);

                await _emailService.SendEmail("Nuevos Postulantes", template, clientEmail);
            }
            else
            {
                legalCase.Status = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.CLOSED;
                await _context.SaveChangesAsync();

                var modelEmail = new StandardEmailModel
                {
                    Title = "Caso Cerrado",
                    SubHeader = "Lamentamos informarte que no hay abogados disponibles en este momento para tu caso. Sin embargo, puedes ingresar a este link para buscar en el directorio a los abogados de tu interés.",
                    LinkName = $"Ir a {ConstantHelpers.PROJECT}",
                    LinkRedirect = $"{ConstantHelpers.GENERAL.PROJECT_URI_BASE}/directorio"
                };

                var template = await _emailTemplateService.GetStandardEmailTemplate(modelEmail);
                await _emailService.SendEmail("Caso Cerrado", template, clientEmail);
            }
        }

        public async Task EndTimeToSelectAndPaymentLawyer(Guid legalCaseId)
        {
            var legalCase = await _context.LegalCases.Where(x => x.Id == legalCaseId).FirstOrDefaultAsync();
            var clientEmail = await _context.Clients.Where(x => x.Id == legalCase.ClientId).Select(x => x.User.Email).FirstOrDefaultAsync();

            var anyLawyer = await _context.LegalCaseLawyers.Where(x => x.LegalCaseId == legalCaseId).AnyAsync();

            if (legalCase.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.SELECTING_LAWYER || legalCase.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.PENDING_PAYMENT)
            {
                legalCase.Status = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.CLOSED;
                var legalCaseLawyers = await _context.LegalCaseLawyers.Where(x => x.LegalCaseId == legalCase.Id).ToListAsync();
                if (legalCaseLawyers.Any())
                {
                    _context.LegalCaseLawyers.RemoveRange(legalCaseLawyers);
                }

                await _context.SaveChangesAsync();

                var modelEmail = new StandardEmailModel
                {
                    Title = "Caso Cerrado",
                    //SubHeader = "Lamentamos informarte que tu caso ha sido cerrado, ya que el tiempo para seleccionar y pagar al abogado ha terminado.",
                    SubHeader = "Muchas gracias por tu confianza, esperamos que hayas podido resolver tus dudas. Si tuvieses alguna consulta adicional puedes contactarnos al correo asesor@legalconnection.pe",
                    LinkName = $"Ir a {ConstantHelpers.PROJECT}",
                    LinkRedirect = $"{ConstantHelpers.GENERAL.PROJECT_URI_BASE}"
                };

                var template = await _emailTemplateService.GetStandardEmailTemplate(modelEmail);
                await _emailService.SendEmail("Caso Cerrado", template, clientEmail);
            }
            else
            {
                var maxHourToSendEmailConfimation = Convert.ToInt32(await _context.Configurations.Where(x => x.Key == ConstantHelpers.CONFIGURATION.MAX_HOUR_TO_SEND_EMAIL_CONFIRMATION).Select(x => x.Value).FirstOrDefaultAsync());
                var maxHoursToCloseLegalCase = Convert.ToInt32(await _context.Configurations.Where(x => x.Key == ConstantHelpers.CONFIGURATION.MAX_HOUR_TO_CLOSE_LEGAL_CASE).Select(x => x.Value).FirstOrDefaultAsync());

                legalCase.Status = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.IN_PROGRESS;

                var jobEmail = BackgroundJob.Schedule(() => SendEmailToConfirmCommunication(legalCaseId), DateTime.UtcNow.AddHours(maxHourToSendEmailConfimation));
                var jobClose = BackgroundJob.Schedule(() => Close(legalCaseId), DateTime.UtcNow.AddHours(maxHoursToCloseLegalCase));

                var legalCaseJobs = new List<LegalCaseDelayedTask>
                {
                    new LegalCaseDelayedTask
                    {
                        Task = ConstantHelpers.ENTITIES.LEGAL_CASE_DELAYED_TASK.TASK.SEND_EMAIL_CONFIRM_COMMUNICATION,
                        HangfireJobId = jobEmail,
                        LegalCaseId = legalCase.Id
                    },
                    new LegalCaseDelayedTask
                    {
                        Task = ConstantHelpers.ENTITIES.LEGAL_CASE_DELAYED_TASK.TASK.CLOSE,
                        HangfireJobId = jobClose,
                        LegalCaseId = legalCase.Id
                    }
                };

                await _context.LegalCaseDelayedTasks.AddRangeAsync(legalCaseJobs);
                await _context.SaveChangesAsync();
            }
        }

        public async Task SendEmailToConfirmCommunication(Guid legalCaseId)
        {
            var legalCase = await _context.LegalCases.Where(x => x.Id == legalCaseId).FirstOrDefaultAsync();
            if (legalCase.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.IN_PROGRESS)
            {
                var clientEmail = await _context.Clients.Where(x => x.Id == legalCase.ClientId).Select(x => x.User.Email).FirstOrDefaultAsync();
                //Se envia un correo
            }
        }

        public async Task Close(Guid legalCaseId)
        {
            var legalCase = await _context.LegalCases.Where(x => x.Id == legalCaseId).FirstOrDefaultAsync();
            legalCase.Status = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.FINALIZED;
            var clientEmail = await _context.Clients.Where(x => x.Id == legalCase.ClientId).Select(x => x.User.Email).FirstOrDefaultAsync();

            var legalCaseLawyers = await _context.LegalCaseLawyers.Where(x => x.LegalCaseId == legalCase.Id).ToListAsync();

            if (legalCaseLawyers.Any(y=>y.Status != ConstantHelpers.ENTITIES.LEGAL_CASE_LAWYER.STATUS.FINISHED))
            {
                foreach (var legalCaseLawyer in legalCaseLawyers.Where(x=>x.Status != ConstantHelpers.ENTITIES.LEGAL_CASE_LAWYER.STATUS.FINISHED))
                {
                    legalCaseLawyer.Status = ConstantHelpers.ENTITIES.LEGAL_CASE_LAWYER.STATUS.FINISHED;
                    var modelEmail = new StandardEmailModel
                    {
                        Title = "Caso Concluido",
                        SubHeader = "Por favor, califica al abogado que te atendió a través del siguiente enlace.",
                        LinkRedirect = $"{ConstantHelpers.GENERAL.PROJECT_URI_BASE}/calificaciones/{legalCase.Id}/{legalCase.ClientId}/{legalCaseLawyer.LawyerId}",
                        LinkName = "Calificar Abogado"
                    };

                    var template = await _emailTemplateService.GetStandardEmailTemplate(modelEmail);
                    await _emailService.SendEmail("Calificar Abogado", template, clientEmail);
                }
            }

            await _context.SaveChangesAsync();
        }

        //

        public async Task EndTimeToLawyerAcceptDirect(Guid legalCaseId)
        {
            var legalCase = await _context.LegalCases.Where(x => x.Id == legalCaseId).FirstOrDefaultAsync();
            var clientEmail = await _context.Clients.Where(x => x.Id == legalCase.ClientId).Select(x => x.User.Email).FirstOrDefaultAsync();

            if(legalCase.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.PENDING_PAYMENT)
            {
                var maxHourToPayLawyer = Convert.ToInt32(await _context.Configurations.Where(x => x.Key == ConstantHelpers.CONFIGURATION.MAX_HOUR_TIME_TO_CLIENT_PAY_LAWYER).Select(x=>x.Value).FirstOrDefaultAsync());
                var job = BackgroundJob.Schedule(() => EndTimeToClientPayLawyer(legalCaseId), DateTime.UtcNow.AddHours(maxHourToPayLawyer));

                var legalCaseJob = new LegalCaseDelayedTask
                {
                    Task = ConstantHelpers.ENTITIES.LEGAL_CASE_DELAYED_TASK.TASK.END_TIME_TO_CLIENT_PAY,
                    HangfireJobId = job,
                    LegalCaseId = legalCase.Id
                };

                await _context.LegalCaseDelayedTasks.AddAsync(legalCaseJob);
                await _context.SaveChangesAsync();
            }
            else
            {
                legalCase.Status = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.CLOSED;
                await _context.SaveChangesAsync();

                var modelEmail = new StandardEmailModel
                {
                    Title = "Caso Cerrado",
                    SubHeader = "Lamentamos informarte que el abogado no ha aceptado el caso. Sin embargo, puedes ingresar a este link para buscar en el directorio a otro abogado de tu interés.",
                    LinkName = $"Ir a {ConstantHelpers.PROJECT}",
                    LinkRedirect = $"{ConstantHelpers.GENERAL.PROJECT_URI_BASE}/directorio"
                };

                var template = await _emailTemplateService.GetStandardEmailTemplate(modelEmail);
                await _emailService.SendEmail("Caso Cerrado", template, clientEmail);
            }
        }

        public async Task EndTimeToClientPayLawyer(Guid legalCaseId)
        {
            var legalCase = await _context.LegalCases.Where(x => x.Id == legalCaseId).FirstOrDefaultAsync();
            var clientEmail = await _context.Clients.Where(x => x.Id == legalCase.ClientId).Select(x => x.User.Email).FirstOrDefaultAsync();

            if(legalCase.Status == ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.PENDING_PAYMENT)
            {
                legalCase.Status = ConstantHelpers.ENTITIES.LEGAL_CASE.STATUS.CLOSED;
                await _context.SaveChangesAsync();

                var modelEmail = new StandardEmailModel
                {
                    Title = "Caso Cerrado",
                    SubHeader = "Lamentamos informarte que el caso ha sido cerrado, ya que no se ha relizado el pago correspondiente.",
                    LinkName = $"Ir a {ConstantHelpers.PROJECT}",
                    LinkRedirect = $"{ConstantHelpers.GENERAL.PROJECT_URI_BASE}/directorio"
                };

                var template = await _emailTemplateService.GetStandardEmailTemplate(modelEmail);
                await _emailService.SendEmail("Caso Cerrado", template, clientEmail);
            }
            else
            {
                var maxHoursToCloseLegalCase = Convert.ToInt32(await _context.Configurations.Where(x => x.Key == ConstantHelpers.CONFIGURATION.MAX_HOUR_TO_CLOSE_LEGAL_CASE).Select(x => x.Value).FirstOrDefaultAsync());
                var jobClose = BackgroundJob.Schedule(() => Close(legalCaseId), DateTime.UtcNow.AddHours(maxHoursToCloseLegalCase));
                var legalCaseJob = new LegalCaseDelayedTask
                {
                    Task = ConstantHelpers.ENTITIES.LEGAL_CASE_DELAYED_TASK.TASK.CLOSE,
                    HangfireJobId = jobClose,
                    LegalCaseId = legalCase.Id
                };

                await _context.LegalCaseDelayedTasks.AddAsync(legalCaseJob);
                await _context.SaveChangesAsync();
            }
        }
    }
}
