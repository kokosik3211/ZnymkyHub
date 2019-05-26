using System;
using ZnymkyHub.DAL.Core.Domain;
using ZnymkyHub.DAL.Core.Repositories;

namespace ZnymkyHub.DAL.Persistence.Repositories
{
    public class AuthorizedUserRepository : Repository<AuthorizedUser>, IAuthorizedUserRepository
    {
        public AuthorizedUserRepository(ZnymkyHubContext context) : base(context) { }
    }
}
