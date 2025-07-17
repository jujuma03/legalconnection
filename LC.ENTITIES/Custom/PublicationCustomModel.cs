using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Custom
{
    public class PublicationCustomModel
    {
        public Guid Id { get; set; }
        public string Topic { get; set; }
        public byte Type { get; set; }

        public ExternalPublicationCustom ExternalPublication { get; set; }
        public LawyerPublicationCustom LawyerPublication { get; set; }
    }
    public class ExternalPublicationCustom
    {
        public string Title { get; set; }
        public string PublicationDate { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
    }
    public class LawyerPublicationCustom
    {
        public string Title { get; set; }
        public string PublicationDate { get; set; }
        public string PhotoUrl { get; set; }
        public string Description { get; set; }
    }

}
