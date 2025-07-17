using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class Payment
    {
        public Guid Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public Guid LegalCaseId { get; set; }
        public LegalCase LegalCase { get; set; }
        public Guid LawyerId { get; set; }
        public Lawyer Lawyer { get; set; }
        public string OnlinePaymentId { get; set; }
        [Column(TypeName = "decimal(19,2)")]
        public decimal DiscountRate { get; set; } //Porcentaje de descuento
        [Column(TypeName = "decimal(19,2)")]
        public decimal IgvAmount { get; set; } //Monto IGV
        [Column(TypeName = "decimal(19,2)")]
        public decimal BaseAmount { get; set; } //Monto sin IGV
        [Column(TypeName = "decimal(19,2)")]
        public decimal DiscountAmount { get; set; } //Monto de descuento (LC)
        [Column(TypeName = "decimal(19,2)")]
        public decimal LawyerAmount { get; set; } //Monto para el abogado
        [Column(TypeName = "decimal(19,2)")]
        public decimal TotalAmount { get; set; } //Monto Total 
        public int Number { get; set; }
        public string Serie { get; set; }
        [NotMapped]
        public string NumberSerie => $"{Serie}-{Number:00000}";
    }
}
