using CalorieTrackerAPI.Models;
using CalorieTrackerAPI.Repositories;
using CalorieTrackerWeb.Models;

namespace CalorieTrackerAPI.Services
{
    public class WorkoutService : IEntryService<Workout>
    {
        private readonly IEntryRepo<Workout> _repo;

        public WorkoutService(IEntryRepo<Workout> repo)
        {
            _repo = repo;
        }

        public void AddEntry(Workout u)
        {
            _repo.AddEntry(u);
        }

        public void DeleteEntry(int id)
        {
            _repo.DeleteEntry(id);
        }

        public List<Workout> GetAllEntries(int uid)
        {
            return _repo.GetAllEntries(uid);
        }

        public Workout GetEntry(int id)
        {
            return _repo.GetEntry(id);
        }

        public void UpdateEntry(int id, Workout u)
        {
            _repo.UpdateEntry(id, u);
        }
    }
}
