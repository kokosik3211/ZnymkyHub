using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using ZnymkyHub.DAL.Core.Repositories;
using ZnymkyHub.DAL.Persistence;

namespace ZnymkyHub.DAL.Core
{
    public interface IUnitOfWork : IDisposable
    {
        ZnymkyHubContext Context { get; }
        IRoleRepository Roles { get; }
        IUserRepository Users { get; }
        IPhotoshootTypeRepository PhotoshootTypes { get; }
        IUserPhotoshootTypeRepository UserPhotoshootTypes { get; }
        IPhotoRepository Photos { get; }
        IPhotoResolutionRepository PhotoResolutions { get; }
        IPhotoshootRepository Photoshoots { get; }
        IOutgoingCityRepository OutgoingCities { get; }
        IPhotographerOutgoingCityRepository PhotographerOutgoingCities { get; }
        ILikeRepository Likes { get; }
        ISavingRepository Savings { get; }
        ICommentRepository Comments { get; }
        IFavouritePhotographerRepository FavouritePhotographers { get; }
        IChatRepository Chats { get; }

        int Commit();
        Task<int> CommitAsync();
    }
}
