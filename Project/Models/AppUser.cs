namespace Project.Models
{
    public class AppUser
    {
        public int UserId { get; set; }
        public string Username { get; set; }
        public string PasswordHash { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public double? DailyCalorieGoal { get; set; }
        public double? DailyProteinGoal { get; set; }
        public double? DailyFatGoal { get; set; }
        public double? DailyCarbohydratesGoal { get; set; }

        public ICollection<UserMeal> UserMeals { get; set; }
    }

    public class UserNutritionalSummary
    {
        public string Username { get; set; }
        public double TotalCalories { get; set; }
        public double TotalProtein { get; set; }
        public double TotalFat { get; set; }
        public double TotalCarbohydrates { get; set; }
        public double TotalWeight { get; set; }
    }
}
