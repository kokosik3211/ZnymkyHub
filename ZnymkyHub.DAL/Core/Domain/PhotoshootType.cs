using System;
using System.Collections.Generic;
using System.Text;

namespace ZnymkyHub.DAL.Core.Domain
{
    public class PhotoshootType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<UserPhotoshootType> PhotoshootTypes { get; set; } = new HashSet<UserPhotoshootType>();

        public virtual ICollection<Photoshoot> Photoshoots { get; set; } = new HashSet<Photoshoot>();
    }
}
