using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class SectionItem
    {
        public Guid Id { get; set; }
        public string UrlImage { get; set; }
        public string HeadLine { get; set; }
        public string Description { get; set; }
        public int Order { get; set; } = 0;
        public byte Status { get; set; }
        //tipos : beneficio / servicio / contactoabogado / contactocliente / nuestro equipo
        public byte Type { get; set; }
    }
}
