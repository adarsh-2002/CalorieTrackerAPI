using CalorieTrackerAPI.Models;

namespace CalorieTrackerAPI.Services
{
    public interface IUserService<U>
    {
        List<U> GetAllUsers();
        U GetUser(int id);
        void AddUser(U u);
        void UpdateUser(int id, U u);
        void DeleteUser(int id);
        U Auth(LoginModel login);


    }
}
