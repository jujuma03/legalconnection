using LC.CORE.Helpers;
using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class LawyerLanguage
    {
        public Guid Id { get; set; }
        public Guid LanguageId { get; set; }
        public Guid LawyerId { get; set; }
        public byte Level { get; set; } = ConstantHelpers.ENTITIES.LAWYER_LANGUAGE.LEVEL.BASIC;
        public byte TemporalStatus { get; set; } = ConstantHelpers.ENTITIES.LAWYER_LANGUAGE.TEMPORAL_STATUS.VALIDATED;
        public Lawyer Lawyer { get; set; }
        public Language Language { get; set; }
    }
}
