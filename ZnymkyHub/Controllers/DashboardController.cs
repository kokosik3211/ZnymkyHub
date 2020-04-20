
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Png;
using SixLabors.ImageSharp.PixelFormats;
using ZnymkyHub.DAL.Core;
using ZnymkyHub.DAL.Core.Domain;
using ZnymkyHub.DTO.Core;

namespace ZnymkyHub.Controllers
{
    [Authorize]
    [Route("api/[controller]/[action]")]
    public class DashboardController : Controller
    {
        private readonly ClaimsPrincipal _caller;
        private readonly IUnitOfWork _unitOfWork;

        public DashboardController(UserManager<User> userManager, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _unitOfWork = unitOfWork;
        }

        // GET api/dashboard/home
        [HttpGet]
        public async Task<IActionResult> Home()
        {
            // retrieve the user info
            var userId = _caller.Claims.Single(c => c.Type == "id");
            var customer = await _unitOfWork.Context.Users.SingleAsync(c => c.Id.ToString() == userId.Value);

            string base64 = null;
            if (customer.ProfilePhoto != null)
            {
                Image<Rgba32> image = Image.Load(customer.ProfilePhoto);
                base64 = image.ToBase64String(PngFormat.Instance);
            }

            var time = DateTime.Now - customer.RegistrationDate;
            string duration = time.Days.ToString();

            return new OkObjectResult(new
            {
                Message = "This is secure API and user data!",
                customer.Id,
                customer.FirstName,
                customer.LastName,
                //customer.Identity.PictureUrl,
                //customer.Identity.FacebookId,
                customer.HomeTown,
                //customer.Locale,
                customer.Gender,
                base64,
                customer.InstagramUrl,
                duration,
                customer.PhoneNumber,
                customer.RoleId
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetFavouritePhotographers()
        {
            var userId = _caller.Claims.FirstOrDefault(c => c.Type == "id");
            if(userId == null)
            {
                return BadRequest();
            }

            var id = Convert.ToInt32(userId.Value);

            var photographers = await (from f in _unitOfWork.Context.FavouritePhotographers
                                 join p in _unitOfWork.Context.Photographers
                                 on f.PhotographerId equals p.Id
                                 where f.UserId == id
                                 select new
                                 {
                                     id = p.Id,
                                     userName = p.UserName,
                                     email = p.Email,
                                     firstName = p.FirstName,
                                     lastName = p.LastName,
                                     homeTown = p.HomeTown,
                                     roleId = p.RoleId,
                                     base64 = p.ProfilePhoto
                                 }).ToListAsync();

            List<SimpleUserDTO> userList = new List<SimpleUserDTO>();
            foreach (var elem in photographers)
            {
                string base64 = null;
                if (elem.base64 != null)
                {
                    Image<Rgba32> image = Image.Load(elem.base64);
                    base64 = image.ToBase64String(PngFormat.Instance);
                }
                userList.Add(new SimpleUserDTO
                {
                    id = elem.id,
                    userName = elem.userName,
                    email = elem.email,
                    firstName = elem.firstName,
                    lastName = elem.lastName,
                    homeTown = elem.homeTown,
                    roleId = elem.roleId,
                    photo = base64,
                    fullName = $"{elem.firstName} {elem.lastName}"
                });
            }
            return Ok(userList);
        }
    }
}
