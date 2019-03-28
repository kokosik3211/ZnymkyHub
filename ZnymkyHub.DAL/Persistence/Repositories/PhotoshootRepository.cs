using System;
using System.Collections.Generic;
using System.Text;
using ZnymkyHub.DAL.Core.Domain;
using ZnymkyHub.DAL.Core.Repositories;

namespace ZnymkyHub.DAL.Persistence.Repositories
{
    public class PhotoshootRepository : Repository<Photoshoot>, IPhotoshootRepository
    {
        public PhotoshootRepository(ZnymkyHubContext context) : base(context) { }
    }
}
