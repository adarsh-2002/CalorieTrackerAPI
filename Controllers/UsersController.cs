using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CalorieTrackerAPI.Models;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;
using CalorieTrackerAPI.Services;

namespace CalorieTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UsersController : ControllerBase
    {
        private readonly IUserService<User> userService;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(UsersController));
        public UsersController(IUserService<User> us)
        {
            userService = us;
        }

        // GET: api/Users
        [HttpGet]
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            _log4net.Info("Get all users method is called");
            return userService.GetAllUsers();
        }

        // GET: api/Users/5
        [HttpGet("Details")]
        public async Task<IActionResult> Details(int id)
        {
            if (id != null)
            {
                var user = userService.GetUser(id);
                if (user == null)
                {
                    return NotFound();
                }
                return Ok(user);
            }
            return BadRequest();
        }

        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, [Bind("Id,Name,Email,Dob,Gender,Height,Weight,RequiredCalories")] User user)
        {
            if (id != user.Id)
            {
                return BadRequest();
            }
            try
            {
                User oUser = new User();
                oUser = userService.GetUser(id);
                if (oUser == null)
                {
                    return NotFound();
                }
                else
                {
                    oUser.Name = user.Name;
                    oUser.Email = user.Email;
                    oUser.Gender = user.Gender;
                    oUser.Weight = user.Weight;
                    oUser.Height = user.Height;
                    oUser.Dob = user.Dob;
                    oUser.RequiredCalories = user.RequiredCalories;
                    userService.UpdateUser(id, oUser);
                }
            }
            catch (DbUpdateConcurrencyException)
            {
                return NotFound();
            }

            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        [Route("Signup")]
        public async Task<ActionResult> SignUp(User user)
        {
            if (user != null)
            {
                if (user.Dob > DateTime.Now)
                {
                    return BadRequest();
                }
                user.salt = RandomNumberGenerator.GetBytes(128 / 8);
                user.Password = Convert.ToBase64String(KeyDerivation.Pbkdf2(
                    password: user.Password,
                    salt: user.salt,
                    prf: KeyDerivationPrf.HMACSHA256,
                    iterationCount: 1000,
                    numBytesRequested: 256 / 8));
                try
                {
                    userService.AddUser(user);
                    _log4net.Info(user.Email + " has signed up!");
                    return Ok();
                }
                catch (DbUpdateException e)
                {
                    Console.WriteLine(e.ToString());
                    return Forbid();
                }
            }
            return BadRequest();
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            var user = userService.GetUser(id);
            if (user == null)
            {
                return NotFound();
            }

            userService.DeleteUser(id);

            return NoContent();
        }
    }
}
