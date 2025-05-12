 using System;
using System.Collections.Generic;
using System.Linq;
using ZooFeedingCostCalculator.Interfaces;
using ZooFeedingCostCalculator.Models;

namespace ZooFeedingCostCalculator.Services
{
    public class FeedingCostCalculator : IFeedingCostCalculator
    {
        public decimal CalculateTotalCost(IEnumerable<AnimalInstance> animals, IEnumerable<AnimalSpecies> species, FoodPrices prices)
        {
            if (animals == null) throw new ArgumentNullException(nameof(animals));
            if (species == null) throw new ArgumentNullException(nameof(species));
            if (prices == null) throw new ArgumentNullException(nameof(prices));

            decimal totalCost = 0m;

            try
            {
                var speciesDict = species.ToDictionary(s => s.Name, StringComparer.OrdinalIgnoreCase);

                foreach (var animal in animals)
                {
                    if (!speciesDict.TryGetValue(animal.SpeciesName, out var animalSpecies))
                    {
                        // Species not found, skip
                        continue;
                    }

                    var foodAmount = animal.WeightKg * animalSpecies.Rate;

                    switch (animalSpecies.FoodType)
                    {
                        case FoodType.Meat:
                            totalCost += foodAmount * prices.MeatPricePerKg;
                            break;
                        case FoodType.Fruit:
                            totalCost += foodAmount * prices.FruitPricePerKg;
                            break;
                        case FoodType.Both:
                            var meatAmount = foodAmount * animalSpecies.MeatPercentage / 100m;
                            var fruitAmount = foodAmount - meatAmount;
                            totalCost += meatAmount * prices.MeatPricePerKg + fruitAmount * prices.FruitPricePerKg;
                            break;
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error calculating total cost: {ex.Message}");
                throw;
            }

            return totalCost;
        }
    }
}
