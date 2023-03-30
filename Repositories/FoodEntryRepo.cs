using CalorieTrackerAPI.Models;
using CalorieTrackerWeb.Models;

namespace CalorieTrackerAPI.Repositories
{
    public class FoodEntryRepo : IEntryRepo<FoodEntry>
    {
        private readonly CalorieTrackerContext _context;
        public FoodEntryRepo(CalorieTrackerContext context) {
            _context = context;
        }
        public void AddEntry(FoodEntry u)
        {
            _context.FoodEntries.Add(u);
            _context.SaveChanges();
        }

        public void DeleteEntry(int id)
        {
            FoodEntry fe = _context.FoodEntries.Find(id);
            _context.FoodEntries.Remove(fe);
            _context.SaveChanges();
        }

        public List<FoodEntry> GetAllEntries(int uid)
        {
            return _context.FoodEntries.Where(x=>x.UserId==uid).ToList();
        }

        public FoodEntry GetEntry(int id)
        {
            return _context.FoodEntries.Find(id);
        }


        public void UpdateEntry(int id, FoodEntry u)
        {
            _context.FoodEntries.Update(u);
            _context.SaveChanges();
        }
       
    }
}
