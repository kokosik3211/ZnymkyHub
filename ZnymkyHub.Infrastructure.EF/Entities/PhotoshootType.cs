using System.Collections.Generic;

namespace ZnymkyHub.Infrastructure.EF.Entities
{
    public class PhotoshootType
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<UserPhotoshootType> UserPhotoshootTypes { get; set; } = new HashSet<UserPhotoshootType>();

        public virtual ICollection<Photoshoot> Photoshoots { get; set; } = new HashSet<Photoshoot>();

        public virtual ICollection<Photo> Photos { get; set; } = new HashSet<Photo>();
    }
}
