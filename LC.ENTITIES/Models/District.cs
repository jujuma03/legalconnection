using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class District
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public Guid ProvinceId { get; set; }
        public Province Province { get; set; }
        public List<ApplicationUser> Users { get; set; }
    }
}
