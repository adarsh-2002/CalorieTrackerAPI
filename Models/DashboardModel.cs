namespace CalorieTrackerWeb.Models
{
    public class DashboardModel
    {
        public IEnumerable<DashboardFoodModel> Foods { get; set; }
        public IEnumerable<DashboardWorkoutModel> Workouts { get; set; }
        public DashboardModel() { }
        public DashboardModel(IEnumerable<DashboardFoodModel> foods, IEnumerable<DashboardWorkoutModel> workouts)
        {
            Foods = foods;
            Workouts = workouts;
        }
    }
}
