using System;
using System.Collections.Generic;
using System.Text;

namespace ZnymkyHub.DAL.Core.Domain
{
    public class OutgoingCity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<PhotographerOutgoingCity> PhotographerOutgoingCities { get; set; } = new HashSet<PhotographerOutgoingCity>();
    }
}
