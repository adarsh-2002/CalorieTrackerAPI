using CalorieTrackerAPI.Models;
using CalorieTrackerAPI.Repositories;

namespace CalorieTrackerAPI.Services
{
    public class UserService : IUserService<User>
    {
        private readonly IUserRepo<User> _repo;
        public UserService(IUserRepo<User> repo) {
            _repo = repo; 
        }
        public void AddUser(User u)
        {
            _repo.AddUser(u);
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
