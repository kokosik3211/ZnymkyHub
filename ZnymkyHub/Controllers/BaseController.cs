
using Microsoft.AspNetCore.Mvc;
using ZnymkyHub.Infrastructure.EF;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ZnymkyHub.Controllers
{
    public class BaseController : Controller
    {
        protected ZnymkyHubContext _dbContext { get; set; }

        public BaseController(ZnymkyHubContext dbContext)
        {
            _dbContext = dbContext;
        }
    }
}
