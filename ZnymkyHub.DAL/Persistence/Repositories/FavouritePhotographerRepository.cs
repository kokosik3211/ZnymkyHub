using System;
using System.Collections.Generic;
using System.Text;
using ZnymkyHub.DAL.Core.Domain;
using ZnymkyHub.DAL.Core.Repositories;

namespace ZnymkyHub.DAL.Persistence.Repositories
{
    public class FavouritePhotographerRepository : Repository<FavouritePhotographer>, IFavouritePhotographerRepository
    {
        public FavouritePhotographerRepository(ZnymkyHubContext context) : base(context) { }
    }
}
