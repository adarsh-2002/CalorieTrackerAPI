using CalorieTrackerAPI.Models;

namespace CalorieTrackerAPI.Services
{
    public interface IEntryService<T>
    {
        List<T> GetAllEntries(int uid);
        T GetEntry(int id);
        void AddEntry(T u);
        void UpdateEntry(int id, T u);
        void DeleteEntry(int id);

    }
}
