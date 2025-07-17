using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class Configuration
    {
        [Key]
        public string Key { get; set; }
        public string Value { get; set; }
    }
}
