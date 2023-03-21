using CalorieTrackerAPI.Models;
using CalorieTrackerAPI.Repositories;
using CalorieTrackerWeb.Models;

namespace CalorieTrackerAPI.Services
{
    public class FoodEntryService : IEntryService<FoodEntry>
    {
        private readonly IEntryRepo<FoodEntry> _entryRepo;
        public FoodEntryService(IEntryRepo<FoodEntry> entryRepo) {
            _entryRepo = entryRepo;
        }
        public void AddEntry(FoodEntry u)
        {
            _entryRepo.AddEntry(u);
        }

        public void DeleteEntry(int id)
        {
            _entryRepo.DeleteEntry(id);
        }

        public List<FoodEntry> GetAllEntries(int uid)
        {
            return _entryRepo.GetAllEntries(uid);
        }

        public FoodEntry GetEntry(int id)
        {
            return _entryRepo.GetEntry(id);
        }

        public void UpdateEntry(int id, FoodEntry u)
        {
            _entryRepo.UpdateEntry(id, u);
        }
    }
}
