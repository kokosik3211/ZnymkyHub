
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
    }
}
