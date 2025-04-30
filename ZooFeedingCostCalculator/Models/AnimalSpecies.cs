namespace ZooFeedingCostCalculator.Models
{
    public enum FoodType
    {
        Meat,
        Fruit,
        Both
    }

    public class AnimalSpecies
    {
        public string Name { get; set; }
        public decimal Rate { get; set; }
        public FoodType FoodType { get; set; }
        public int MeatPercentage { get; set; } // For omnivores, 0-100
    }
}
