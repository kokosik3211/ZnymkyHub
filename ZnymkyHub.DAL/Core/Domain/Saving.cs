
namespace ZnymkyHub.DAL.Core.Domain
{
    public class Saving
    {
        public int Id { get; set; }

        public int PhotoId { get; set; }
        public virtual Photo Photo { get; set; }

        public int UserId { get; set; }
        public virtual User User { get; set; }
    }
}
