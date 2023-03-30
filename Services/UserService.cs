using CalorieTrackerAPI.Controllers;
using CalorieTrackerAPI.Models;
using CalorieTrackerAPI.Repositories;
using Microsoft.AspNetCore.Cryptography.KeyDerivation;

namespace CalorieTrackerAPI.Services
{
    public class UserService : IUserService<User>
    {
        private readonly IUserRepo<User> _repo;
        static readonly log4net.ILog _log4net = log4net.LogManager.GetLogger(typeof(UserService));
        public UserService(IUserRepo<User> repo) {
            _repo = repo; 
        }
        public void AddUser(User u)
        {
            _repo.AddUser(u);
        }

        public User Auth(LoginModel lm)
        {
            if (lm.Email != null && lm.Password != null)
            {
                User result = new();
                result = _repo.GetAllUsers().SingleOrDefault(x => x.Email == lm.Email);
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
                        _log4net.Info(lm.Email + " has logged in!");
                        return result;
                    }
                }
            }
            return null;
        }

        public void DeleteUser(int id)
        {
            _repo.DeleteUser(id);
        }

        public List<User> GetAllUsers()
        {
            return _repo.GetAllUsers();
        }

        public User GetUser(int id)
        {
            return _repo.GetUser(id);
        }

        public void UpdateUser(int id, User u)
        {
            _repo.UpdateUser(id, u);
        }
    }
}
