using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ZnymkyHub.DAL.Core;
using ZnymkyHub.DAL.Core.Domain;

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

        public async Task<IActionResult> GetUser()
        {
            var user = await _userManager.FindByIdAsync("2");
            return Ok(new
            {
                Id = user.Id,
                Name = $"{user.FirstName} {user.LastName}",
                Image = user.ProfilePhoto,
                City = user.HomeTown
            });
        }
    }
}
