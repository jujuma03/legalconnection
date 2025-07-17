using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class TemporalLawyer
    {
        [Key]
        public Guid LawyerId { get; set; }
        public string CAL { get; set; }
        [Column(TypeName = "decimal(19,2)")]
        public decimal Fee { get; set; }
        public bool FreeFirst { get; set; }
        public string AboutMe { get; set; }
        public string Name { get; set; }
        public string Surnames { get; set; }
        public string Document { get; set; }
        public string HouseNumber { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? BirthDate { get; set; }
        public byte Sex { get; set; }
        public string Picture { get; set; }
        public Guid? DistrictId { get; set; }
        public byte DocumentType { get; set; }
        [ForeignKey("LawyerId")]
        public Lawyer Lawyer { get; set; }
    }
}
