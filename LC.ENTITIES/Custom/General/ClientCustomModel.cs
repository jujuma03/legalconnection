using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Custom.General
{
    public class ClientCustomModel
    {
        public string Name { get; set; }
        public string Surnames { get; set; }
        public string FullName => $"{Name} {Surnames}";
        public string Department { get; set; }
        public string Province { get; set; }
        public string PhoneNumber { get; set; }
        public string PhotoUrl { get; set; }
        public string Email { get; set; }
        public string Document { get; set; }
    }
}
