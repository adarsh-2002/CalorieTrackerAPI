using CalorieTrackerAPI.Models;
using CalorieTrackerAPI.Services;
using CalorieTrackerWeb.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CalorieTrackerAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IEntryService<FoodEntry> _fEntryService;
        private readonly IEntryService<Workout> _wEntryService;

        public HomeController(IEntryService<FoodEntry> fEntryService, IEntryService<Workout> wEntryService)
        {
            _fEntryService = fEntryService;
            _wEntryService = wEntryService;
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> Dashboard(int id)
        {
            if (id != null)
            {
                //query the context to find total calories in a given day
                var feResults = (from fe in _fEntryService.GetAllEntries(id).OrderByDescending(x => x.Date)
                                 group fe by fe.Date.Date into feGroup
                                 select new
                                 {
                                     Date = feGroup.Key,
                                     TotalCal = feGroup.Sum(x => x.Calories),
                                     TotalProteins = feGroup.Sum(x => x.Proteins),
                                     TotalCarbs = feGroup.Sum(x => x.Carbs),
                                     TotalFats = feGroup.Sum(x => x.Fats)
                                 }).ToList();
                var wResults = (from w in _wEntryService.GetAllEntries(id).OrderByDescending(x => x.WDateTime)
                                group w by w.WDateTime.Date into wGroup
                                select new
                                {
                                    Date = wGroup.Key,
                                    TotalWCal = wGroup.Sum(x => x.Calories)
                                }).ToList();
                Console.WriteLine(feResults.Count);
                Console.WriteLine(wResults.Count);
                List<DashboardFoodModel> FoodObj = new List<DashboardFoodModel>();
                foreach (var item in feResults)
                {
                    DashboardFoodModel dfm = new DashboardFoodModel();
                    dfm.Date = item.Date;
                    dfm.TotalCal = item.TotalCal;
                    dfm.TotalPro = item.TotalProteins;
                    dfm.TotalCar = item.TotalCarbs;
                    dfm.TotalFat = item.TotalFats;
                    FoodObj.Add(dfm);
                }
                List<DashboardWorkoutModel> WorkoutObj = new List<DashboardWorkoutModel>();
                foreach (var item in wResults)
                {
                    DashboardWorkoutModel dwm = new DashboardWorkoutModel();
                    dwm.Date = item.Date;
                    dwm.TotalWCals = item.TotalWCal;
                    WorkoutObj.Add(dwm);
                }
                DashboardModel model = new DashboardModel(FoodObj.Take(10), WorkoutObj.Take(10));
                return Ok(model);
            }
            return Unauthorized();
        }
    }
}
