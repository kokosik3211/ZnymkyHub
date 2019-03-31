using Microsoft.EntityFrameworkCore;
using ZnymkyHub.DAL.Core.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ZnymkyHub.DAL.Persistence.EntityConfigurations;

namespace ZnymkyHub.DAL.Persistence
{
    public class ZnymkyHubContext : IdentityDbContext<User,Role,int> 
    {
        public ZnymkyHubContext(DbContextOptions<ZnymkyHubContext> options) : base(options) { }

        public virtual DbSet<PhotoshootType> PhotoshootTypes { get; set; }
        public virtual DbSet<UserPhotoshootType> UserPhotoshootTypes { get; set; }
        public virtual DbSet<Photo> Photos { get; set; }
        public virtual DbSet<PhotoResolution> PhotoResolutions { get; set; }
        public virtual DbSet<Photoshoot> Photoshoots { get; set; }
        public virtual DbSet<OutgoingCity> OutgoingCities { get; set; }
        public virtual DbSet<PhotographerOutgoingCity> PhotographerOutgoingCities { get; set; }
        public virtual DbSet<Like> Likes { get; set; }
        public virtual DbSet<Saving> Savings { get; set; }
        public virtual DbSet<Comment> Comments { get; set; }
        public virtual DbSet<FavouritePhotographer> FavouritePhotographers { get; set; }
        public virtual DbSet<Chat> Chats { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseLazyLoadingProxies();
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);

            builder.ApplyConfiguration(new RoleConfigurations());
            builder.ApplyConfiguration(new UserConfigurations());
            builder.ApplyConfiguration(new PhotoshootTypeConfigurations());
            builder.ApplyConfiguration(new UserPhotoshootTypeConfigurations());
            builder.ApplyConfiguration(new PhotoResolutionConfigurations());
            builder.ApplyConfiguration(new PhotoshootConfigurations());
            builder.ApplyConfiguration(new PhotoConfigurations());
            builder.ApplyConfiguration(new OutgoingCityConfigurations());
            builder.ApplyConfiguration(new PhotographerOutgoingCityConfiguration());
            builder.ApplyConfiguration(new LikeConfiguration());
            builder.ApplyConfiguration(new SavingConfiguration());
            builder.ApplyConfiguration(new CommentConfiguration());
            builder.ApplyConfiguration(new FavouritePhotographerConfiguration());
            builder.ApplyConfiguration(new ChatConfiguration());
        }
    }
}
