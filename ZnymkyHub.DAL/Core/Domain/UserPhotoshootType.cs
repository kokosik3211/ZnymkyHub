using System;
using System.Collections.Generic;
using System.Text;

namespace ZnymkyHub.DAL.Core.Domain
{
    public class UserPhotoshootType
    {
        public int Id { get; set; }

        public int PhotographerId { get; set; }
        public virtual Photographer Photographer { get; set; }

        public int PhotoshootTypeId { get; set; }
        public virtual PhotoshootType PhotoshootType { get; set; }

        public int Price { get; set; }
    }
}
