using System;
using System.Collections.Generic;
using System.Text;

namespace LC.ENTITIES.Models
{
    public class Shortcut
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string UrlDirection { get; set; }
        public byte Type { get; set; }
        public byte Status { get; set; }
        public Guid? ParentShortcutId { get; set; }
        public Shortcut ParentShortcut { get; set; }
    }
}
