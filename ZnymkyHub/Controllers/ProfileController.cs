using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZnymkyHub.DAL.Core;
using ZnymkyHub.DAL.Core.Domain;

namespace ZnymkyHub.Controllers
{
    [Authorize(Policy = "ApiUser")]
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
        [HttpGet]
        public async Task<IActionResult> Me()
        {
            // retrieve the user info
            var userId = _caller.Claims.Single(c => c.Type == "id");
            var customer = await _unitOfWork.Context.Users.SingleAsync(c => c.Id.ToString() == userId.Value);

            return new OkObjectResult(new
            {
                customer.FirstName,
                customer.LastName,
                //customer.Identity.PictureUrl,
                //customer.Identity.FacebookId,
                customer.HomeTown,
                //customer.Locale,
                customer.Gender,
                customer.Email
            });
        }
    }
}
