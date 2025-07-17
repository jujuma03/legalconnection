using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Custom
{
    public class EconomicManagementLawyerCustomModel
    {
        public Guid LawyerId { get; set; }
        public int Applications { get; set; }
        public int PendingDirected { get; set; }
        public int InCourse { get; set; }
        public int Finalized{ get; set; }
    }
}
