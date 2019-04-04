using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.AspNetCore.Identity;

namespace ZnymkyHub.DAL.Core.Domain
{
    public enum Gender : byte
    {
        Male, Female, Other
    }

    public class User : IdentityUser<int>
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public int RoleId { get; set; }
        public virtual Role Role { get; set; }

        public string HomeTown { get; set; }

        public string ProfilePhoto { get; set; }

        public string ProfilePhotoName { get; set; }

        public DateTime RegistrationDate { get; set; }

        public string FacebookUrl { get; set; }
        public string InstagramUrl { get; set; }

        public virtual ICollection<Like> Likes { get; set; } = new HashSet<Like>();

        public virtual ICollection<Saving> Savings { get; set; } = new HashSet<Saving>();

        public virtual ICollection<Comment> Comments { get; set; } = new HashSet<Comment>();

        public virtual ICollection<FavouritePhotographer> FavouritePhotographerPhotographers { get; set; } = new HashSet<FavouritePhotographer>();

        public virtual ICollection<Chat> ChatSender { get; set; } = new HashSet<Chat>();

        public virtual ICollection<Chat> ChatReceiver { get; set; } = new HashSet<Chat>();

    }

    public class Photographer : User
    {
        public int NumberOfPhotos { get; set; }

        public virtual ICollection<UserPhotoshootType> UserPhotoshootTypes { get; set; } = new HashSet<UserPhotoshootType>();

        public virtual ICollection<Photoshoot> Photoshoots { get; set; } = new HashSet<Photoshoot>();

        public virtual ICollection<Photo> Photos { get; set; } = new HashSet<Photo>();

        public virtual ICollection<PhotographerOutgoingCity> PhotographerOutgoingCities { get; set; } = new HashSet<PhotographerOutgoingCity>();

        public virtual ICollection<FavouritePhotographer> FavouritePhotographerUsers { get; set; } = new HashSet<FavouritePhotographer>();

    }

    public class AuthorizedUser : User
    {

    }
}
