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
        public LoginController(IUserService<User> us) {
            userService = us;
        }

        // POST api/<LoginController>
        [HttpPost]
        public IActionResult Post(LoginModel lm)
        {
            if (lm.Email != null && lm.Password != null)
            {
                var result = userService.GetAllUsers().SingleOrDefault(x => x.Email == lm.Email);
                if (result != null)
                {
                    string hashed = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                                               password: lm.Password,
                                               salt: result.salt,
                                               prf: KeyDerivationPrf.HMACSHA256,
                                               iterationCount: 1000,
                                               numBytesRequested: 256 / 8));
                    if (hashed == result.Password)
                    {
                        return Ok(result);
                    }
                }
            }
            return Unauthorized();
        }
    }
}
