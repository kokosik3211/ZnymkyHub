
using System.Collections.Generic;

namespace ZnymkyHub.Infrastructure.EF.Entities
{
    public class OutgoingCity
    {
        public int Id { get; set; }

        public string Name { get; set; }

        public virtual ICollection<PhotographerOutgoingCity> PhotographerOutgoingCities { get; set; } = new HashSet<PhotographerOutgoingCity>();
    }
}
