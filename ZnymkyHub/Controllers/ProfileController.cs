using System;
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

namespace ZnymkyHub.Controllers
{
    [Route("api/[controller]/[action]")]
    public class ProfileController : Controller
    {
        private readonly ClaimsPrincipal _caller;
        private readonly IUnitOfWork _unitOfWork;

        public ProfileController(UserManager<User> userManager, IUnitOfWork unitOfWork, IHttpContextAccessor httpContextAccessor)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _unitOfWork = unitOfWork;
        }

        // GET api/profile/me
        [Authorize(Policy = "ApiUser")]
        [HttpGet]
        public async Task<IActionResult> Me()
        {
            // retrieve the user info
            var userId = _caller.Claims.Single(c => c.Type == "id");
            var customer = await _unitOfWork.Context.Users.SingleAsync(c => c.Id.ToString() == userId.Value);

            return new OkObjectResult(new
            {
                customer.Id,
                customer.FirstName,
                customer.LastName,
                //customer.Identity.PictureUrl,
                //customer.Identity.FacebookId,
                customer.HomeTown,
                //customer.Locale,
                customer.Gender,
                customer.Email,
                customer.RoleId
            });
        }

        [HttpGet]
        public async Task<IActionResult> GetUserProfileInfo(int id)
        {
            var user = await _unitOfWork.Context.Users.SingleAsync(c => c.Id == id);

            var userId = _caller.Claims.FirstOrDefault(c => c.Type == "id");
            var activity = new ProfileActivityDAO
            {
                ProfileId = id,
                VisitorId = userId != null ? Convert.ToInt32(userId.Value): default(int?),
                Date = DateTime.Now
            };
            await _unitOfWork.Context.ProfileActivities.AddAsync(activity);
            await _unitOfWork.CommitAsync();

            string base64 = null;
            if (user.ProfilePhoto != null)
            {
                Image<Rgba32> image = Image.Load(user.ProfilePhoto);
                base64 = image.ToBase64String(PngFormat.Instance);
            }

            var time = DateTime.Now - user.RegistrationDate;
            string duration = time.Days.ToString();

            return new OkObjectResult(new
            {
                user.Id,
                user.FirstName,
                user.LastName,
                user.HomeTown,
                user.Gender,
                base64,
                user.InstagramUrl,
                duration,
                user.PhoneNumber
            });
        }
    }
}
