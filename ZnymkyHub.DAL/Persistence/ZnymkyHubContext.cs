using Microsoft.EntityFrameworkCore;
using ZnymkyHub.DAL.Core.Domain;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using ZnymkyHub.DAL.Persistence.EntityConfigurations;

namespace ZnymkyHub.DAL.Persistence
{
    public class ZnymkyHubContext : IdentityDbContext<User,Role,int> 
    {
        private bool isEmptyConstructor = false;
        public ZnymkyHubContext() {
            this.isEmptyConstructor = true;
        }
        public ZnymkyHubContext(DbContextOptions<ZnymkyHubContext> options) : base(options) { }

        public virtual DbSet<Photographer> Photographers { get; set; }
        public virtual DbSet<AuthorizedUser> AuthorizedUsers { get; set; }
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

            if (isEmptyConstructor)
            {
                string connection = System.Runtime.InteropServices.RuntimeInformation.IsOSPlatform(System.Runtime.InteropServices.OSPlatform.Windows)
                ? "Data Source=localhost\\SQLEXPRESS;Initial Catalog=ZnymkyHubDB;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"
                : "Data Source=localhost;Initial Catalog=ZnymkyHubDB;User ID=sa;Password=YourStrong@Passw0rd;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                optionsBuilder.UseSqlServer(connection);
            }

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
