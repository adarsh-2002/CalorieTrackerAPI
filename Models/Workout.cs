using System.ComponentModel.DataAnnotations;

namespace CalorieTrackerAPI.Models
{
    public class Workout
    {
        [Key]
        public int Id { get; set; }
        public int UserId { get; set; }
        public virtual User? User { get; set; }
        [Display(Name = "Workout Date")]
        public DateTime WDateTime { get; set; }
        public string Name { get; set; }
        [Range(minimum: 0, maximum: int.MaxValue)]
        public float Calories { get; set; }
        public Workout() { }
        public Workout(int userId, string name, int calories)
        {
            UserId = userId;
            Name = name;
            Calories = calories;
        }
    }
}
