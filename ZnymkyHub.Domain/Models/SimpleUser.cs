using System;
namespace ZnymkyHub.Domain.Models
{
    public class SimpleUser
    {
        public int id { get; set; }
        public string userName { get; set; }
        public string email { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string homeTown { get; set; }
        public int roleId { get; set; }
        public string photo { get; set; }
        public string fullName { get; set; }
    }
}
