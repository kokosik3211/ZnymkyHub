using System;
using System.Collections.Generic;
using System.Text;

namespace ZnymkyHub.DAL.Core.Domain
{
    public class FavouritePhotographer
    {
        public int Id { get; set; }

        public int PhotographerId { get; set; }
        public virtual Photographer Photographer { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
