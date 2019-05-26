using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace ZnymkyHub.DTO.Core
{
    public class ChangeUserProfilePhotoDTO
    {
        public IFormFile newimage { get; set; }

        public string id { get; set; }
    }
}
