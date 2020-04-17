using System;

namespace ZnymkyHub.DAL.Core.Domain
{
    public class ProfileActivityDAO
    {
        public int Id { get; set; }
        public int ProfileId { get; set; }
        public int? VisitorId { get; set; }
        public DateTime Date { get; set; }
    }
}
