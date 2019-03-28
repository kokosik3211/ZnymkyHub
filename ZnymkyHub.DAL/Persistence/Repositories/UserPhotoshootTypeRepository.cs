using System;
using System.Collections.Generic;
using System.Text;
using ZnymkyHub.DAL.Core.Domain;
using ZnymkyHub.DAL.Core.Repositories;

namespace ZnymkyHub.DAL.Persistence.Repositories
{
    public class UserPhotoshootTypeRepository : Repository<UserPhotoshootType>, IUserPhotoshootTypeRepository
    {
        public UserPhotoshootTypeRepository(ZnymkyHubContext context) : base(context) { }
    }
}
