namespace Project.Models
{
    public class AppUser
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public float? DailyCalorieGoal { get; set; }
        public float? DailyProteinGoal { get; set; }
        public float? DailyFatGoal { get; set; }
        public float? DailyCarbohydratesGoal { get; set; }

        public ICollection<UserMeal> UserMeals { get; set; }
    }
}
