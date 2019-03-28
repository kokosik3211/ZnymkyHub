using System;
using System.Collections.Generic;
using System.Text;
using ZnymkyHub.DAL.Core.Domain;
using ZnymkyHub.DAL.Core.Repositories;

namespace ZnymkyHub.DAL.Persistence.Repositories
{
    public class SavingRepository : Repository<Saving>, ISavingRepository
    {
        public SavingRepository(ZnymkyHubContext context) : base(context) { }
    }
}
