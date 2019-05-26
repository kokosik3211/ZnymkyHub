using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using ZnymkyHub.DAL.Core;
using ZnymkyHub.DAL.Core.Domain;
using ZnymkyHub.DTO.Core;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZnymkyHub.Controllers
{
    [Route("api/[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly UserManager<User> _userManager;

        public UserController(IUnitOfWork unitOfWork, UserManager<User> userManager)
        {
            _unitOfWork = unitOfWork;
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> GetUser()
        {
            var user = await _userManager.FindByIdAsync("2");
            return Ok(new
            {
                id = user.Id,
                userName = user.UserName,
                email = user.Email,
                firstName = user.FirstName,
                lastName = user.LastName,
                homeTown = user.HomeTown,
                roleId = user.RoleId,
                photo = user.ProfilePhoto,
                fullName = $"{user.FirstName} {user.LastName}"
            });
        }

        [HttpPost]
        public async Task<IActionResult> GetTestUser()
        {
            var user = await _userManager.FindByIdAsync("3");
            return Ok(new
            {
                id = user.Id,
                userName = user.UserName,
                email = user.Email,
                firstName = user.FirstName,
                lastName = user.LastName,
                homeTown = user.HomeTown,
                roleId = user.RoleId,
                photo = user.ProfilePhoto,
                fullName = $"{user.FirstName} {user.LastName}"
            });
        }

        [HttpPost]
        public async Task<IActionResult> PhotographersForMain()
        {
            var users = _unitOfWork.Context.Photographers.Take(12).ToList();
            List<SimpleUserDTO> userList = new List<SimpleUserDTO>();
            foreach(var elem in users)
            {
                string base64 = null;
                if(elem.ProfilePhoto != null)
                {
                    Image<Rgba32> image = Image.Load(elem.ProfilePhoto);
                    base64 = image.ToBase64String(PngFormat.Instance);
                }
                userList.Add(new SimpleUserDTO
                {
                    id = elem.Id,
                    userName = elem.UserName,
                    email = elem.Email,
                    firstName = elem.FirstName,
                    lastName = elem.LastName,
                    homeTown = elem.HomeTown,
                    roleId = elem.RoleId,
                    photo = base64,
                    fullName = $"{elem.FirstName} {elem.LastName}"
                });
            }
            return Ok(userList);
        }

        [HttpPost]
        public async Task<IActionResult> PhotographersCount()
        {
            var count = _unitOfWork.Context.Photographers.Count();

            return Ok(count);
        }

        [HttpPost]
        public IActionResult SearchPhotographer(string city, string photoshootType)
        {
            //var ph = _unitOfWork.Context.Photographers.Where(p => p.PhotographerOutgoingCities.)
            return Ok();
        }
    }
}
