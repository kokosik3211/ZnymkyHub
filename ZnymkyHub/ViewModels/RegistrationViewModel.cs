
namespace ZnymkyHub.ViewModels
{
    public class RegistrationViewModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string HomeTown { get; set; }
        public int RoleId { get; set; }
        public bool EmailConfirmed { get; set; } = true;
        public string InstagramUrl { get; set; } = null;
    }
}
