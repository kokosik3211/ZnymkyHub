using System;
using System.Collections.Generic;
using System.Text;
using ZnymkyHub.DAL.Core.Repositories;

namespace ZnymkyHub.DAL.Core
{
    interface IUnitOfWork : IDisposable
    {
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
    }
}
