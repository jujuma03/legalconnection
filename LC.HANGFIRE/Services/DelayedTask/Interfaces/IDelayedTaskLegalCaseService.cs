using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LC.HANGFIRE.Services.DelayedTask.Interfaces
{
    public interface IDelayedTaskLegalCaseService
    {
        Task EndApplicationTime(Guid legalCaseId);
        Task EndTimeToSelectAndPaymentLawyer(Guid legalCaseId);
        Task SendEmailToConfirmCommunication(Guid legalCaseId);
        Task Close(Guid legalCaseId);
        Task EndTimeToLawyerAcceptDirect(Guid legalCaseId);
        Task EndTimeToClientPayLawyer(Guid legalCaseId);
    }
}
