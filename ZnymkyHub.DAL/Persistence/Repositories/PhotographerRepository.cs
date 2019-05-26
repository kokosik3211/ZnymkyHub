using System;
using ZnymkyHub.DAL.Core.Domain;
using ZnymkyHub.DAL.Core.Repositories;

namespace ZnymkyHub.DAL.Persistence.Repositories
{
    public class PhotographerRepository : Repository<Photographer>, IPhotographerRepository
    {
        public PhotographerRepository(ZnymkyHubContext context) : base(context) { }
    }
}
