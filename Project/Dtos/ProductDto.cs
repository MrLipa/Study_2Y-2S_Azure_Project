namespace Project.Models
{
    public class ProductDto
    {
        public int ProductId { get; set; }
        public string? Name { get; set; }
        public float Calories { get; set; }
        public float Protein { get; set; }
        public float Fat { get; set; }
        public float Carbohydrates { get; set; }
    }
}
