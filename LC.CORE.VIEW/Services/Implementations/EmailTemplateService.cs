using LC.CORE.Services.Interfaces;
using LC.CORE.VIEW.Services.Interfaces;
using LC.CORE.VIEW.Services.Models.Email;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace LC.CORE.VIEW.Services.Implementations
{
    public class EmailTemplateService : IEmailTemplateService
    {
        private readonly IViewRenderService _viewRenderService;

        public EmailTemplateService(IViewRenderService viewRenderService)
        {
            _viewRenderService = viewRenderService;
        }
        public async Task<string> GetStandardEmailTemplate(StandardEmailModel model)
        {
            var result = await _viewRenderService.RenderToStringAsync("/Views/Email/StandardEmailView.cshtml", model);
            return result;
        }
        public async Task<string> GetLawyerPostulantToCaseEmailTemplate(LawyerPostulantToCaseEmailModel model)
        {
            var result = await _viewRenderService.RenderToStringAsync("/Views/Email/LawyerPostulantToCaseEmailView.cshtml", model);
            return result;
        }

        public async Task<string> GetReceiptEmailTemplate(ReceiptEmailModel model)
        {
            var result = await _viewRenderService.RenderToStringAsync("/Views/Email/ReceiptEmailView.cshtml", model);
            return result;
        }

        public async Task<string> GetConfirmationEmailTemplate(ConfirmationEmailModel model)
        {
            var result = await _viewRenderService.RenderToStringAsync("/Views/Email/ConfirmationEmailView.cshtml", model);
            return result;
        }

        public async Task<string> GetLawyerRejectedEmailTemplate(LawyerRejectedEmailModel model)
        {
            var result = await _viewRenderService.RenderToStringAsync("/Views/Email/LawyerRejectedEmailView.cshtml", model);
            return result;
        }

        public async Task<string> GetLawyerInterviewEmailTemplate(LawyerInterviewEmailModel model)
        {
            var result = await _viewRenderService.RenderToStringAsync("/Views/Email/LawyerInterviewEmailView.cshtml", model);
            return result;
        }

        public async Task<string> GetNewLawyerPaymentEmailTemplate(NewLawyerPaymentEmailModel model)
        {
            var result = await _viewRenderService.RenderToStringAsync("/Views/Email/NewLawyerPaymentEmailView.cshtml", model);
            return result;
        }
    }
}
