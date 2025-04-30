using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using ZooFeedingCostCalculator.Interfaces;
using ZooFeedingCostCalculator.Models;

namespace ZooFeedingCostCalculator.Services
{
    public class AnimalSpeciesProvider : IAnimalSpeciesProvider
    {
        public IEnumerable<AnimalSpecies> GetAnimalSpecies(string filePath)
        {
            var speciesList = new List<AnimalSpecies>();

            var lines = File.ReadAllLines(filePath);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line))
                    continue;

                var parts = line.Split(';');
                if (parts.Length < 3)
                    continue;

                var name = parts[0].Trim();
                var rate = decimal.Parse(parts[1].Trim(), CultureInfo.InvariantCulture);
                var foodTypeStr = parts[2].Trim().ToLowerInvariant();

                FoodType foodType;
                switch (foodTypeStr)
                {
                    case "meat":
                        foodType = FoodType.Meat;
                        break;
                    case "fruit":
                        foodType = FoodType.Fruit;
                        break;
                    case "both":
                        foodType = FoodType.Both;
                        break;
                    default:
                        throw new Exception($"Unknown food type: {foodTypeStr}");
                }

                int meatPercentage = 0;
                if (foodType == FoodType.Both && parts.Length >= 4)
                {
                    var percentStr = parts[3].Trim().TrimEnd('%');
                    meatPercentage = int.Parse(percentStr);
                }

                speciesList.Add(new AnimalSpecies
                {
                    Name = name,
                    Rate = rate,
                    FoodType = foodType,
                    MeatPercentage = meatPercentage
                });
            }

            return speciesList;
        }
    }
}
