using System.Collections.Generic;
using Xunit;
using ZooFeedingCostCalculator.Models;
using ZooFeedingCostCalculator.Services;

namespace ZooFeedingCostCalculator.Tests
{
    public class FeedingCostCalculatorTests
    {
        [Fact]
        public void CalculateTotalCost_CalculatesCorrectly()
        {
            // Arrange
            var animals = new List<AnimalInstance>
            {
                new AnimalInstance { SpeciesName = "Lion", Name = "Simba", WeightKg = 160 },
                new AnimalInstance { SpeciesName = "Wolf", Name = "Pin", WeightKg = 78 }
            };

            var species = new List<AnimalSpecies>
            {
                new AnimalSpecies { Name = "Lion", Rate = 0.10m, FoodType = FoodType.Meat },
                new AnimalSpecies { Name = "Wolf", Rate = 0.07m, FoodType = FoodType.Both, MeatPercentage = 90 }
            };

            var prices = new FoodPrices
            {
                MeatPricePerKg = 12.56m,
                FruitPricePerKg = 5.60m
            };

            var calculator = new FeedingCostCalculator();

            // Act
            var totalCost = calculator.CalculateTotalCost(animals, species, prices);

            // Lion: 160 * 0.10 = 16 kg meat * 12.56 = 200.96
            // Wolf: 78 * 0.07 = 5.46 kg total food
            //       Meat: 90% of 5.46 = 4.914 kg * 12.56 = 61.7
            //       Fruit: 10% of 5.46 = 0.546 kg * 5.60 = 3.06
            // Total = 200.96 + 61.7 + 3.06 = 265.72

            // Assert
            Assert.Equal(265.72m, totalCost, 2);
        }
    }
}
