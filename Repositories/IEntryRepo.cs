using CalorieTrackerAPI.Models;

namespace CalorieTrackerAPI.Repositories
{
    public interface IEntryRepo<T>
    {
        List<T> GetAllEntries(int uid);
        T GetEntry(int id);
        void AddEntry(T u);
        void UpdateEntry(int id, T u);
        void DeleteEntry(int id);
    }
}
