using System;
using Microsoft.AspNetCore.Http;
using Newtonsoft.Json;

namespace ZnymkyHub.DTO.Core
{
    public class UploadPhotoDTO
    {
        [JsonProperty("name")]
        public string name { get; set; }
        [JsonProperty("pType")]
        public string pType { get; set; }
        [JsonProperty("newimage")]
        public IFormFile pFile { get; set; }
    }
}
