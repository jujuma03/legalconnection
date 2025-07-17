using LC.CORE.VIEW.Services.Models.Email;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.CORE.VIEW.Services.Interfaces
{
    public interface IEmailTemplateService
    {
        Task<string> GetStandardEmailTemplate(StandardEmailModel model);
        Task<string> GetLawyerPostulantToCaseEmailTemplate(LawyerPostulantToCaseEmailModel model);
        Task<string> GetReceiptEmailTemplate(ReceiptEmailModel model);
        Task<string> GetConfirmationEmailTemplate(ConfirmationEmailModel model);
        Task<string> GetLawyerRejectedEmailTemplate(LawyerRejectedEmailModel model);
        Task<string> GetLawyerInterviewEmailTemplate(LawyerInterviewEmailModel model);
        Task<string> GetNewLawyerPaymentEmailTemplate(NewLawyerPaymentEmailModel model);
    }
}
