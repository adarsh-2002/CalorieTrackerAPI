using CalorieTrackerAPI.Models;

namespace CalorieTrackerAPI.Services
{
    public interface IUserService<User>
    {
        List<User> GetAllUsers();
        User GetUser(int id);
        void AddUser(User u);
        void UpdateUser(int id, User u);
        void DeleteUser(int id);
    }
}
