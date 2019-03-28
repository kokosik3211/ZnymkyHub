using System;
using System.Collections.Generic;
using System.Text;
using ZnymkyHub.DAL.Core;
using ZnymkyHub.DAL.Core.Repositories;
using ZnymkyHub.DAL.Persistence.Repositories;

namespace ZnymkyHub.DAL.Persistence
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ZnymkyHubContext _context;

        public UnitOfWork (ZnymkyHubContext context)
        {
            _context = context;
            Roles = new RoleRepository(_context);
            Users = new UserRepository(_context);
            PhotoshootTypes = new PhotoshootTypeRepository(_context);
            UserPhotoshootTypes = new UserPhotoshootTypeRepository(_context);
            Photos = new PhotoRepository(_context);
            PhotoResolutions = new PhotoResolutionRepository(_context);
            Photoshoots = new PhotoshootRepository(_context);
            OutgoingCities = new OutgoingCityRepository(_context);
            PhotographerOutgoingCities = new PhotographerOutgoingCityRepository(_context);
            Likes = new LikeRepository(_context);
            Savings = new SavingRepository(_context);
            Comments = new CommentRepository(_context);
            FavouritePhotographers = new FavouritePhotographerRepository(_context);
            Chats = new ChatRepository(_context);
        }

        public IRoleRepository Roles { get; private set; }
        public IUserRepository Users { get; private set; }
        public IPhotoshootTypeRepository PhotoshootTypes { get; private set; }
        public IUserPhotoshootTypeRepository UserPhotoshootTypes { get; private set; }
        public IPhotoRepository Photos { get; private set; }
        public IPhotoResolutionRepository PhotoResolutions { get; private set; }
        public IPhotoshootRepository Photoshoots { get; private set; }
        public IOutgoingCityRepository OutgoingCities { get; private set; }
        public IPhotographerOutgoingCityRepository PhotographerOutgoingCities { get; private set; }
        public ILikeRepository Likes { get; private set; }
        public ISavingRepository Savings { get; private set; }
        public ICommentRepository Comments { get; private set; }
        public IFavouritePhotographerRepository FavouritePhotographers { get; private set; }
        public IChatRepository Chats { get; private set; }

        public int Commit()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
