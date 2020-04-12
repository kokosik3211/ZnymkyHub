
namespace ZnymkyHub.DAL.Core.Domain
{
    public class PhotographerOutgoingCity
    {
        public int Id { get; set; }

        public int PhotographerId { get; set; }
        public virtual Photographer Photographer { get; set; }

        public int OutgoingCityId { get; set; }
        public virtual OutgoingCity OutgoingCity { get; set; }

        public int AdditionalPayment { get; set; }


    }
}
