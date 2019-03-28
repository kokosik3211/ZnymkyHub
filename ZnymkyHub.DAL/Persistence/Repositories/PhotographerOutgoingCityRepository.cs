using System;
using System.Collections.Generic;
using System.Text;
using ZnymkyHub.DAL.Core.Domain;
using ZnymkyHub.DAL.Core.Repositories;

namespace ZnymkyHub.DAL.Persistence.Repositories
{
    public class PhotographerOutgoingCityRepository : Repository<PhotographerOutgoingCity>, IPhotographerOutgoingCityRepository
    {
        public PhotographerOutgoingCityRepository(ZnymkyHubContext context) : base(context) { }
    }
}
