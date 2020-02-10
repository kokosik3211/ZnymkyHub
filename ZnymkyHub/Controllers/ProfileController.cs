
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using ZnymkyHub.Infrastructure.EF.Entities;
using ZnymkyHub.Infrastructure.EF;

namespace ZnymkyHub.Controllers
{
    [Authorize(Policy = "ApiUser")]
    [Route("api/[controller]/[action]")]
    public class ProfileController : Controller
    {
        private readonly ClaimsPrincipal _caller;
        private readonly ZnymkyHubContext _dbContext;

        public ProfileController(UserManager<User> userManager, ZnymkyHubContext dbContext, IHttpContextAccessor httpContextAccessor)
        {
            _caller = httpContextAccessor.HttpContext.User;
            _dbContext = dbContext;
        }

        // GET api/profile/me
        [HttpGet]
        public async Task<IActionResult> Me()
        {
            // retrieve the user info
            var userId = _caller.Claims.Single(c => c.Type == "id");
            var customer = await _dbContext.Users.SingleAsync(c => c.Id.ToString() == userId.Value);

            return new OkObjectResult(new
            {
                customer.FirstName,
                customer.LastName,
                //customer.Identity.PictureUrl,
                //customer.Identity.FacebookId,
                customer.HomeTown,
                //customer.Locale,
                customer.Gender
            });
        }
    }
}
