using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Custom
{
    public class LawyerCustomModel
    {
        public Guid Id { get; set; }
        public string FullName { get; set; }
        public string PostulationDate { get; set; }
        public bool You { get; set; }
    }
}
