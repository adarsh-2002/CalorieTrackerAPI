using CalorieTrackerAPI.Models;
using CalorieTrackerAPI.Services;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CalorieTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        private readonly IUserService<User> userService;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(LoginController));

        public LoginController(IUserService<User> us) {
            userService = us;
        }

        // POST api/<LoginController>
        [HttpPost]
        public IActionResult Post(LoginModel lm)
        {
            User u = new User();
            u = userService.Auth(lm);
            if (u!=null)
            {
                return Ok(u);
            }
            return Unauthorized();
        }
    }
}
