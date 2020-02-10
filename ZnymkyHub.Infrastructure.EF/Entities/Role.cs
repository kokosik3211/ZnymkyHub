using System.Collections.Generic;
using Microsoft.AspNetCore.Identity;

// роль - адмін, фотограф чи користувач
namespace ZnymkyHub.Infrastructure.EF.Entities
{
    public class Role : IdentityRole<int> //int - тип ключа 
    {
        public virtual ICollection<User> Users {get; set;} = new HashSet<User>();

        public Role(string name) : base(name) { }
    }
}
