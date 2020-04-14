using System;
using System.Collections.Generic;

namespace ZnymkyHub.DTO.Core
{
    public class SimpleSearchResultDTO
    {
        public int id { get; set; }
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string location { get; set; }
        public int price { get; set; }
        public string fullName { get; set; }
        public string base64 { get; set; }
        public string instagramUrl { get; set; }
        public List<SimplePhotoDTO> photos { get; set; }
    }
}
