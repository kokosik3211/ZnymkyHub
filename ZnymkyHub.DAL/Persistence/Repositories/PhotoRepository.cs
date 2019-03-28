using System;
using System.Collections.Generic;
using System.Text;
using ZnymkyHub.DAL.Core.Domain;
using ZnymkyHub.DAL.Core.Repositories;

namespace ZnymkyHub.DAL.Persistence.Repositories
{
    public class PhotoRepository : Repository<Photo>, IPhotoRepository
    {
        public PhotoRepository(ZnymkyHubContext context) : base(context) { }
    }
}
