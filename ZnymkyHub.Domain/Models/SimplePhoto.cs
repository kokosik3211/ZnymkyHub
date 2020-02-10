
namespace ZnymkyHub.Domain.Models
{
    public class SimplePhoto
    {
        public int id { get; set; }
        public string base64 { get; set; }
        public int numlikes { get; set; }
        public int numsaves { get; set; }
        public string name { get; set; }
        public string phtype { get; set; }
    }
}
