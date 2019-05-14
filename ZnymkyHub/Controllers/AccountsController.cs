using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ZnymkyHub.DAL.Core;
using ZnymkyHub.DAL.Core.Domain;
using ZnymkyHub.Helpers;
using ZnymkyHub.ViewModels;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZnymkyHub.Controllers
{
    [Route("api/[controller]")]
    public class AccountsController : BaseController
    {
        private readonly UserManager<User> _userManager;
        private readonly IMapper _mapper;

        public AccountsController(UserManager<User> userManager, IMapper mapper, IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            _userManager = userManager;
            _mapper = mapper;
        }

        // POST api/accounts
        [HttpPost]
        public async Task<IActionResult> Post([FromBody]RegistrationViewModel model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var userIdentity = //_mapper.Map<User>(model);
                new User
                {
                    UserName = model.Email,
                    FirstName = model.FirstName,
                    LastName = model.LastName,
                    Email = model.Email,
                    HomeTown = model.HomeTown,
                    RoleId = 3,
                    InstagramUrl = model.InstagramUrl,
                    EmailConfirmed = model.EmailConfirmed
                };

            var result = await _userManager.CreateAsync(userIdentity, model.Password);

            if (!result.Succeeded) return new BadRequestObjectResult(Errors.AddErrorsToModelState(result, ModelState));

            var user = await _userManager.FindByEmailAsync(model.Email);
            var add_role = await _userManager.AddToRoleAsync(user, user.Role.Name);

            //await _unitOfWork.Users.AddAsync(new Customer { IdentityId = userIdentity.Id, Location = model.Location });
            //await _unitOfWork.CommitAsync();

            return new OkObjectResult("Account created");
        }
    }
}
