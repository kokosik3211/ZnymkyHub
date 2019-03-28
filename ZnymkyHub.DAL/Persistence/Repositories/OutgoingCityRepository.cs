using System;
using System.Collections.Generic;
using System.Text;
using ZnymkyHub.DAL.Core.Domain;
using ZnymkyHub.DAL.Core.Repositories;

namespace ZnymkyHub.DAL.Persistence.Repositories
{
    public class OutgoingCityRepository : Repository<OutgoingCity>, IOutgoingCityRepository
    {
        public OutgoingCityRepository(ZnymkyHubContext context) : base(context) { }
    }
}
