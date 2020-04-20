using System;
namespace ZnymkyHub.DTO.Core
{
    public class SimplePhotoDTO
    {
        public int id { get; set; }
        public string base64 { get; set; }
        public int numlikes { get; set; }
        public int numsaves { get; set; }
        public string name { get; set; }
        public string phtype { get; set; }
        public string date { get; set; }
        public bool liked { get; set; }
        public bool saved { get; set; }
        public string phName { get; set; }
        public string phInstagram { get; set; }
    }
}
