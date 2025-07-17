using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class Department
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public List<Province> Provinces { get; set; }
    }
}
