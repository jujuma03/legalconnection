using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Custom.General
{
    public class LawyerCustomModel
    {
        public string Name { get; set; }
        public string Surname { get; set; }
        public string FullName => $"{Name} {Surname}";
        public string PhotoUrl { get; set; }
    }
}
