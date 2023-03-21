using CalorieTrackerAPI.Models;

namespace CalorieTrackerAPI.Repositories
{
    public class UserRepo : IUserRepo<User>
    {
        private readonly CalorieTrackerContext _context;
        public UserRepo(CalorieTrackerContext context)
        {
            _context = context;
        }

        public void AddUser(User u)
        {
            _context.Users.Add(u);
            _context.SaveChanges();
        }

        public void DeleteUser(int id)
        {
            User u = _context.Users.Find(id);
            _context.Users.Remove(u);
            _context.SaveChanges();
        }

        public List<User> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public User GetUser(int id)
        {
            return _context.Users.Find(id);
        }

        public void UpdateUser(int id, User u)
        {
            _context.Users.Update(u);
            _context.SaveChanges();
        }
    }
}
