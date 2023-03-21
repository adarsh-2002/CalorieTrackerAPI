using CalorieTrackerAPI.Models;
using CalorieTrackerWeb.Models;

namespace CalorieTrackerAPI.Repositories
{
    public class WorkoutRepo : IEntryRepo<Workout>
    {
        private readonly CalorieTrackerContext _context;
        public WorkoutRepo(CalorieTrackerContext calorieTrackerContext) {
            _context = calorieTrackerContext;
        }
        public void AddEntry(Workout u)
        {
            _context.Add(u);
            _context.SaveChanges();
        }

        public void DeleteEntry(int id)
        {
            Workout w = _context.Workouts.Find(id);
            _context.Remove(w);
            _context.SaveChanges();
        }

        public List<Workout> GetAllEntries(int uid)
        {
            return _context.Workouts.Where(x => x.UserId == uid).ToList();
        }

        public Workout GetEntry(int id)
        {
            return _context.Workouts.Find(id);
        }

        public void UpdateEntry(int id, Workout u)
        {
            _context.Workouts.Update(u);
            _context.SaveChanges();
        }
    }
}
