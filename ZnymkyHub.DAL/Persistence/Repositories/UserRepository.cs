using System;
using System.Collections.Generic;
using System.Text;
using ZnymkyHub.DAL.Core.Domain;
using ZnymkyHub.DAL.Core.Repositories;

namespace ZnymkyHub.DAL.Persistence.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(ZnymkyHubContext context) : base(context) { }
    }
}
