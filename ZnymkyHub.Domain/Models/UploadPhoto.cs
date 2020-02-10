using Newtonsoft.Json;

namespace ZnymkyHub.Domain.Models
{
    public class UploadPhoto
    {
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("pType")]
        public string pType { get; set; }
        //[JsonProperty("newimage")]
        //public IFormFile pFile { get; set; }
    }
}
