using System;
using System.Collections.Generic;
using System.Text;

namespace ZnymkyHub.DAL.Core.Domain
{
    public class Photoshoot
    {
        public int Id { get; set; }

        public int PhotographerId { get; set; }
        public virtual Photographer Photographer { get; set; }

        public int PhotoshootTypeId { get; set; }
        public virtual PhotoshootType PhotoshootType { get; set; }

        public string Name { get; set; }

        public string Description { get; set; }

        public virtual ICollection<Photo> Photos { get; set; } = new HashSet<Photo>();
    }
}
