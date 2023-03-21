using CalorieTrackerAPI.Models;

namespace CalorieTrackerAPI.Repositories
{
    public interface IUserRepo<User>
    {
        List<User> GetAllUsers();
        User GetUser(int id);
        void AddUser(User u);
        void UpdateUser(int id, User u);
        void DeleteUser(int id);
    }
}
