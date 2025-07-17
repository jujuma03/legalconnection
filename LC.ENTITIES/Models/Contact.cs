using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class Contact
    {
        public class ContactViewData
        {
            public Guid Id { get; set; }
            public string Phone { get; set; }
            public string Celphone { get; set; }
            public string Location { get; set; }
            public string Email { get; set; }
            public string Description { get; set; }
            public string Url { get; set; }
        }
    }
}
