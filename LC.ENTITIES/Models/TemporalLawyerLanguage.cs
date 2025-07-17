using LC.CORE.Helpers;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class TemporalLawyerLanguage
    {
        [Key]
        public Guid? LawyerLanguageId { get; set; }
        public Guid LanguageId { get; set; }
        public Guid LawyerId { get; set; }
        public byte Level { get; set; } = ConstantHelpers.ENTITIES.LAWYER_LANGUAGE.LEVEL.BASIC;
        public Lawyer Lawyer { get; set; }
        public Language Language { get; set; }
        [ForeignKey("LawyerLanguageId")]
        public LawyerLanguage LawyerLanguage { get; set; }
    }
}
