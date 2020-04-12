using System;
using System.Collections.Generic;

namespace ZnymkyHub.DAL.Core.Domain
{
    public class Photo
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public int PhotographerId { get; set; }
        public virtual Photographer Photographer { get; set; }

        public int? PhotoshootId { get; set; }
        public virtual Photoshoot Photoshoot { get; set; }

        public int? PhotoshootTypeId { get; set; }
        public virtual PhotoshootType PhotoshootType { get; set; }

        public virtual PhotoResolution PhotoResolution { get; set; }

        public DateTime DateTime { get; set; }

        public int NumberOfLikes { get; set; }

        public int NumberOfSaving { get; set; }

        public virtual ICollection<Like> Likes { get; set; } = new HashSet<Like>();

        public virtual ICollection<Saving> Savings { get; set; } = new HashSet<Saving>();

        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();
    }
}
