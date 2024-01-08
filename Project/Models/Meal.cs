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
        public float Calories { get; set; }
        public float Protein { get; set; }
        public float Fat { get; set; }
        public float Carbohydrates { get; set; }
        public float QuantityInGrams { get; set; }
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
