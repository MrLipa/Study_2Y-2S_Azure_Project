namespace Project.Models
{
    public class Meal
    {
        public int MealId { get; set; }
        public string Name { get; set; }

        public ICollection<UserMeal> UserMeals { get; set; }
        public ICollection<MealProduct> MealProducts { get; set; }
    }

    public class ProductInfoo
    {
        public string ProductName { get; set; }
        public double Calories { get; set; }
        public double Protein { get; set; }
        public double Fat { get; set; }
        public double Carbohydrates { get; set; }
        public double QuantityInGrams { get; set; }
    }

    public class NutritionalSummary
    {
        public string MealName { get; set; }
        public double TotalCalories { get; set; }
        public double TotalProtein { get; set; }
        public double TotalFat { get; set; }
        public double TotalCarbohydrates { get; set; }
        public double TotalWeight { get; set; }
    }
}
