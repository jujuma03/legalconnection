using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class SocialNetwork
    {
        public Guid Id { get; set; }
        public string UrlDirection { get; set; }
        public byte Status { get; set; }
        public byte Type { get; set; }
    }
}
