namespace Project.Models
{
    public class MealProduct
    {
        public int MealId { get; set; }
        public int ProductId { get; set; }
        public float QuantityInGrams { get; set; }

        public Meal Meal { get; set; }
        public Product Product { get; set; }
    }
}
