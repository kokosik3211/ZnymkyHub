using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using ZnymkyHub.DAL.Core.Domain;

namespace ZnymkyHub.DAL.Persistence
{
    public static class IdentityDataInitializer
    {
        public static async Task SeedData(UserManager<User> userManager, RoleManager<Role> roleManager)
        {
            await SeedRoles(roleManager);
            await SeedUsers(userManager);
        }

        public static async Task SeedUsers(UserManager<User> userManager)
        {
            IdentityResult userResult;
            const string GeneralPassword = "123";
            List<User> users = new List<User>();
            users.Add(new User() { UserName = "romanchuk@gmail.com", FirstName = "Valentyna", LastName = "Romanchuk", Email = "romanchuk@gmail.com", EmailConfirmed = true, RoleId = 1 });
            users.Add(new Photographer() { UserName = "serniuk@gmail.com", FirstName = "Oleh", LastName = "Serniuk", Email = "serniuk@gmail.com", EmailConfirmed = true, RoleId = 2, HomeTown = "Lviv" });
            users.Add(new Photographer() { UserName = "kravtsova@gmail.com", FirstName = "Olena", LastName = "Kravtsova", Email = "kravtsova@gmail.com", EmailConfirmed = true, RoleId = 2, HomeTown = "Lviv" });
            users.Add(new Photographer() { UserName = "franchuk@gmail.com", FirstName = "Natalia", LastName = "Franchuk", Email = "franchuk@gmail.com", EmailConfirmed = true, RoleId = 2, HomeTown = "Lviv" });
            users.Add(new Photographer() { UserName = "kravkun@gmail.com", FirstName = "Olena", LastName = "Kravkun", Email = "kravkun@gmail.com", EmailConfirmed = true, RoleId = 2, HomeTown = "Lviv" });
            users.Add(new Photographer() { UserName = "chornii@gmail.com", FirstName = "Ihor", LastName = "Chornii", Email = "chornii@gmail.com", EmailConfirmed = true, RoleId = 2, HomeTown = "Lviv" });
            users.Add(new Photographer() { UserName = "noha@gmail.com", FirstName = "Taras", LastName = "Noha", Email = "noha@gmail.com", EmailConfirmed = true, RoleId = 2, HomeTown = "Lviv" });
            users.Add(new Photographer() { UserName = "efimov@gmail.com", FirstName = "Ihor", LastName = "Efimov", Email = "efimov@gmail.com", EmailConfirmed = true, RoleId = 2, HomeTown = "Cherkasy" });
            users.Add(new Photographer() { UserName = "ladanivskii@gmail.com", FirstName = "Oleksandr", LastName = "Ladanivskii", Email = "ladanivskii@gmail.com", EmailConfirmed = true, RoleId = 2, HomeTown = "Lviv" });
            users.Add(new Photographer() { UserName = "stisoviak@gmail.com", FirstName = "Artem", LastName = "Stisoviak", Email = "stisoviak@gmail.com", EmailConfirmed = true, RoleId = 2, HomeTown = "Odessa" });
            users.Add(new Photographer() { UserName = "kraist@gmail.com", FirstName = "Yulia", LastName = "Kraist", Email = "kraist@gmail.com", EmailConfirmed = true, RoleId = 2, HomeTown = "Kiev" });
            users.Add(new Photographer() { UserName = "rafalovskyi@gmail.com", FirstName = "Evhenii", LastName = "Rafalovskyi", Email = "rafalovskyi@gmail.com", EmailConfirmed = true, RoleId = 2, HomeTown = "Kiev" });
            users.Add(new Photographer() { UserName = "hytra@gmail.com", FirstName = "Maksym", LastName = "Hytra", Email = "hytra@gmail.com", EmailConfirmed = true, RoleId = 2, HomeTown = "Lviv" });
            users.Add(new Photographer() { UserName = "typolenko@gmail.com", FirstName = "Ihor", LastName = "Typolenko", Email = "typolenko@gmail.com", EmailConfirmed = true, RoleId = 2, HomeTown = "Kiev" });

            foreach (var user in users)
            {
                var userExist = await userManager.FindByEmailAsync(user.Email);
                if (userExist == null)
                {
                    userResult = await userManager.CreateAsync(user, GeneralPassword);
                    var add_role = await userManager.AddToRoleAsync(user, user.Role.Name);
                }
            }
        }

        public static async Task SeedRoles(RoleManager<Role> roleManager)
        {
            string[] roleNames = { "Admin", "Photographer", "AuthorizedUser" };
            IdentityResult roleResult;
            foreach (var role in roleNames)
            {
                var roleExist = await roleManager.RoleExistsAsync(role);
                if (!roleExist)
                {
                    roleResult = await roleManager.CreateAsync(new Role(role));
                }
            }
        }
    }
}
